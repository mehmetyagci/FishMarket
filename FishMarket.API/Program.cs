using AutoMapper;
using FishMarket.API.Filters;
using FishMarket.API.Middlewares;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Data;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FishMarket.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.Service;
using NLayer.Service.Infrastructure;
using System;
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
            builder.Services.AddSwaggerGen();

            #region Auto Mapper Configuration
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            #endregion Auto Mapper Configuration

            builder.Services.AddDbContext<FishMarketDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(FishMarketDbContext)).GetName().Name);
                });
            });

            builder.Services.AddSingleton<IValidator<FishCreateDto>, FishCreateDtoValidator>();
            builder.Services.AddSingleton<IValidator<FishUpdateDto>, FishUpdateDtoValidator>();

            builder.Services.AddTransient(typeof(IService<,,,>), typeof(Service<,,,>));
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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}