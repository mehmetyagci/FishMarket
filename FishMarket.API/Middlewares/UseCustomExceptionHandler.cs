using FishMarket.Dto;
using Microsoft.AspNetCore.Diagnostics;
using NLayer.Service.Exceptions;
using System.Text.Json;
using FluentValidation;

namespace FishMarket.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
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
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var errorMessage = GetFullErrorMessage(exceptionFeature.Error);
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
