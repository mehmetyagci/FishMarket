using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http.Headers;

namespace FishMarket.Web.Filters
{
    public class JwtAuthorizationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContextAccessor = context.HttpContext.RequestServices.GetService<IHttpContextAccessor>();
            var httpClient = context.HttpContext.RequestServices.GetService<HttpClient>();
            var jwtToken = httpContextAccessor.HttpContext.Request.Cookies["JwtToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
