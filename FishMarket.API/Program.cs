using FishMarket.Service.Filters;
using FishMarket.API.Middlewares;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Data;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FishMarket.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FishMarket.Service.Helpers;
using FishMarket.Service.Infrastructure;
using FishMarket.Service.Services;
using System.Reflection;

namespace FishMarket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
                .AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()))
                .AddFluentValidation();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fish Market API", Version = "v1" });
            });

            #region Auto Mapper Configuration
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            #endregion Auto Mapper Configuration

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddDbContext<FishMarketDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(FishMarketDbContext)).GetName().Name);
                });
            });

            builder.Services.AddTransient<IValidator<FishCreateDto>, FishCreateDtoValidator>();
            builder.Services.AddTransient<IValidator<FishUpdateDto>, FishUpdateDtoValidator>();

            builder.Services.AddTransient<IValidator<UserCreateDto>, UserCreateDtoValidator>();
            builder.Services.AddTransient<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
            builder.Services.AddTransient<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddTransient<IImageService, ImageService>();
            builder.Services.AddTransient<IEmailService, EmailService>();


            builder.Services.AddTransient(typeof(IService<,,,>), typeof(Service<,,,>));

            builder.Services.AddTransient(typeof(IFishService), typeof(FishService));
            builder.Services.AddTransient(typeof(IUserService), typeof(UserService));

            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCustomException();
            app.UseStaticFiles();    

            app.UseMiddleware<JWTMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}