using FishMarket.Dto;
using FishMarket.Service.Filters;
using FishMarket.Web.Helpers;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Web.Controllers
{
    [FMAllowAnonymous]
    public class UserController : FMWebController
    {
        private readonly IUserApiService _userApiService;
        private readonly TokenService _tokenService;

        public UserController(
            IUserApiService userApiService, IConfiguration configuration, TokenService tokenService)
             : base(configuration)
        {
            _userApiService = userApiService;
            _tokenService = tokenService;
        }

        private IActionResult RedirectIfLoggedIn()
        {
            if (HttpContext?.Request?.Cookies != null)
            {
                var tokenValue = HttpContext.Request.Cookies["JwtToken"];
                if (!string.IsNullOrEmpty(tokenValue))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return null;
        }

        public async Task<IActionResult> Register()
        {
            IActionResult redirectResult = RedirectIfLoggedIn();
            if (redirectResult != null)
            {
                return redirectResult;
            }
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _userApiService.RegisterAsync(userRegisterDto);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Failed to register user. Please try again.");
                ViewBag.ErrorMessage = "Failed to register user. Please try again.";
            }
            return View(userRegisterDto);
        }


        [HttpGet("verifyemail")]
        public async Task<IActionResult> VerifyEmailGet([FromQuery] UserVerifyEmailDto userVerifyEmailDto)
        {
            IActionResult redirectResult = RedirectIfLoggedIn();
            if (redirectResult != null)
            {
                return redirectResult;
            }

            return View("VerifyEmail", userVerifyEmailDto);
        }

        [HttpPost("verifyemail")]
        public async Task<IActionResult> VerifyEmail(UserVerifyEmailDto userVerifyEmailDto)
        {
            if (string.IsNullOrEmpty(userVerifyEmailDto.Email) || string.IsNullOrEmpty(userVerifyEmailDto.Token))
            {
                ModelState.AddModelError("", "Email and token are required for email verification.");
                ViewBag.ErrorMessage = "Email and token are required for email verification.";
            }

            var result = await _userApiService.VerifyEmail(userVerifyEmailDto);
            if (result)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ModelState.AddModelError("", "Failed to verify email. Please try again.");
                ViewBag.ErrorMessage = "Failed to verify email. Please try again.";
            }
            return View("VerifyEmail", userVerifyEmailDto);
        }


        public async Task<IActionResult> Login()
        {
            IActionResult redirectResult = RedirectIfLoggedIn();
            if (redirectResult != null)
            {
                return redirectResult;
            }

            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthenticateRequestDto userAuthenticateRequestDto)
        {
            var result = await _userApiService.AuthenticateAsync(userAuthenticateRequestDto);
            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                _tokenService.SetTokenCookie(result, HttpContext);
                return RedirectToAction("Index", "Fish");
            }
            else
            {
                ModelState.AddModelError("", "Failed to login. Please try again.");
                ViewBag.ErrorMessage = "Failed to login user. Please try again.";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (Response?.Cookies != null)
            {
                Response.Cookies.Delete("JwtToken");
                Response.Cookies.Delete("UserEmail");
            }
            return await Task.FromResult(RedirectToAction("Index", "Home"));
        }
    }
}