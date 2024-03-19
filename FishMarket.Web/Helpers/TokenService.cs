namespace FishMarket.Web.Helpers
{
    public class TokenService
    {
        public void SetTokenCookie(string token, HttpContext httpContext)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,  
                Secure = true, 
                SameSite = SameSiteMode.Strict, 
                Expires = DateTimeOffset.UtcNow.AddDays(7) 
            };
            httpContext.Response.Cookies.Append("JwtToken", token, cookieOptions);
        }
    }
}
