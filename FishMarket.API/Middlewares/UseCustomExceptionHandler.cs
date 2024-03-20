using FishMarket.Dto;
using Microsoft.AspNetCore.Diagnostics;
using FishMarket.Service.Exceptions;
using System.Text.Json;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FishMarket.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app, ILogger logger) 
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundExcepiton => 404,
                        ValidationException => 400,
                        UnauthorizedAccessException => 401,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    //TODO: 500 Server hataları için, detay yaplaşmayan bir hata fırlat ve hatayı logla.
                    var errorMessage = GetFullErrorMessage(exceptionFeature.Error);
                    logger.LogError(errorMessage, "An unhandled exception occurred.");
                    var response = ResponseDto<NoContentDto>.Fail(statusCode, errorMessage);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }

        private static string GetFullErrorMessage(Exception exception)
        {
            if (exception == null)
                return string.Empty;

            var message = exception.Message;
            if (exception.InnerException != null)
                message += Environment.NewLine + GetFullErrorMessage(exception.InnerException);

            return message;
        }
    }
}
