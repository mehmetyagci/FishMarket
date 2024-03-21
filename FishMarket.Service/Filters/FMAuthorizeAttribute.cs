using FishMarket.Core.Services;
using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FishMarket.Service.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FMAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly bool _forAPI;
        public FMAuthorizeAttribute(bool forAPI = false)
        {
            _forAPI = forAPI;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<FMAllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            if (_forAPI)
            {
                var user = (UserDto)context.HttpContext.Items["User"];
                if (user == null)
                    context.Result = new UnauthorizedObjectResult(ResponseDto<NoContentDto>.Fail(401, "Unauthorized"));
            } 
            else
            {
                var serviceProvider = context.HttpContext.RequestServices;
                var jwtService = serviceProvider.GetService(typeof(IJwtService)) as IJwtService;

                var jwtToken = context.HttpContext.Request.Cookies["JwtToken"];
                var userEmail = context.HttpContext.Request.Cookies["UserEmail"];

                if (string.IsNullOrEmpty(jwtToken))
                {
                    if(userEmail != null && !string.IsNullOrEmpty(userEmail))
                    {
                        context.HttpContext.Response.Cookies.Delete("UserEmail");
                    }
                    context.Result = new RedirectToActionResult("Login", "User", null);
                    return;
                }

                var userId = jwtService.ValidateToken(jwtToken);
                if (!userId.HasValue)
                {
                    context.HttpContext.Response.Cookies.Delete("JwtToken");
                    if (userEmail != null && !string.IsNullOrEmpty(userEmail))
                    {
                        context.HttpContext.Response.Cookies.Delete("UserEmail");
                    }
                    context.Result = new RedirectToActionResult("Login", "User", null);
                }
            }
        }
    }
}
