using FishMarket.Core.Services;

namespace FishMarket.API.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtService.ValidateToken(token);
            if (userId != null)
            {
                var response = await userService.GetByIdAsync(userId.Value);
                context.Items["User"] = response.Data;
            }
            await _next(context);
        }
    }
}
