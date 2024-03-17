using Azure;
using FishMarket.Domain;
using FishMarket.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace FishMarket.API.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FMAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<FMAllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        //var errorMessage = GetFullErrorMessage(exceptionFeature.Error);
        //var response = ResponseDto<NoContentDto>.Fail(statusCode, errorMessage);
        //await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
