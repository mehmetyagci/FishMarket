using AutoMapper;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Data;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Repository;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Auto Mapper Configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AllowNullDestinationValues = true;
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            #endregion Auto Mapper Configuration

            builder.Services.AddDbContext<FishMarketDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(FishMarketDbContext)).GetName().Name);
                });
            });

            builder.Services.AddTransient(typeof(IService<,,,>), typeof(Service<,,,>));
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            //builder.Services.AddScoped(typeof(IService<FishMarket.Domain.Fish, FishMarket.Dto.FishDto>), typeof(Service<FishMarket.Domain.Fish, FishMarket.Dto.FishDto>));

            builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}