using FishMarket.Core.Services;
using FishMarket.Service.Helpers;
using FishMarket.Service.Infrastructure;
using FishMarket.Service.Services;
using FishMarket.Web.Helpers;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FishMarket.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddLog4Net("log4net.config");

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient<FishApiService>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["API:Url"] + "api/");
            });

            builder.Services.AddHttpClient<UserApiService>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["API:Url"] + "api/");
            });

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 5 * 1024 * 1024; // 5 MB in bytes
            });


            #region Auto Mapper Configuration
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            #endregion Auto Mapper Configuration

            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddTransient<TokenService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}