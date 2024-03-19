using FishMarket.Service.Infrastructure;
using FishMarket.Web.Filters;
using FishMarket.Web.Helpers;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Http.Features;

namespace FishMarket.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddTransient<TokenService>();

            builder.Services.AddScoped<JwtAuthorizationFilter>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}