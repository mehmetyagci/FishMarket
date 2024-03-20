using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FishMarket.Service.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class FMAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly bool _forApi;
        public FMAuthorizeAttribute(bool forApi = false)
        {
            _forApi = forApi;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<FMAllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var serviceProvider = context.HttpContext.RequestServices;
            var jwtService = serviceProvider.GetService(typeof(IJwtService)) as IJwtService;

            var jwtToken = context.HttpContext.Request.Cookies["JwtToken"];
            if (string.IsNullOrEmpty(jwtToken))
            {
                if (_forApi)
                {
                    context.Result = new UnauthorizedObjectResult(ResponseDto<NoContentDto>.Fail(401, "Unauthorized"));
                }
                else
                {
                    context.Result = new RedirectToActionResult("Login", "User", null);
                }
                return;
            }

            var userId = jwtService.ValidateToken(jwtToken);
            if (!userId.HasValue)
            {
                if (_forApi)
                {
                    context.Result = new UnauthorizedObjectResult(ResponseDto<NoContentDto>.Fail(401, "Unauthorized"));
                }
                else
                {
                    context.Result = new RedirectToActionResult("Login", "User", null);
                }
                return;
            }

            //var user = (UserDto)context.HttpContext.Items["User"];
            //if (user == null)
            //    context.Result = new UnauthorizedObjectResult(ResponseDto<NoContentDto>.Fail(401, "Unauthorized"));
        }
    }
}
