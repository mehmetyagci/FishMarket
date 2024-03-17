using FishMarket.Domain;
using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FishMarket.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FMAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<FMAllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = (UserDto)context.HttpContext.Items["User"];
            if (user == null)
                context.Result = new BadRequestObjectResult(ResponseDto<NoContentDto>.Fail(StatusCodes.Status401Unauthorized, "Unauthorized"));
        }
    }
}
