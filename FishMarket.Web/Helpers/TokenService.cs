using FishMarket.Dto;

namespace FishMarket.Web.Helpers
{
    public class TokenService
    {
        public void SetTokenCookie(UserAuthenticateResponseDto response, HttpContext httpContext)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,  
                Secure = true, 
                SameSite = SameSiteMode.Strict, 
                Expires = DateTimeOffset.UtcNow.AddDays(7) 
            };
            httpContext.Response.Cookies.Append("JwtToken", response.Token, cookieOptions);
            httpContext.Response.Cookies.Append("UserEmail", response.Email, cookieOptions);
        }
    }
}
