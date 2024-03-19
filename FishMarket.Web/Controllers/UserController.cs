using FishMarket.Dto;
using FishMarket.Web.Helpers;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Web.Controllers
{
    public class UserController : FMWebController
    {
        private readonly UserApiService _userApiService;
        private readonly TokenService _tokenService;

        public UserController(
            UserApiService userApiService, IConfiguration configuration, TokenService tokenService)
             : base(configuration)
        {
            _userApiService = userApiService;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var token = await _userApiService.RegisterAsync(userRegisterDto);
            return RedirectToAction("VerifyEmail", "User");
        }

        public async Task<IActionResult> VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(UserVerifyEmailDto userVerifyEmailDto)
        {
            var token = await _userApiService.VerifyEmail(userVerifyEmailDto);
            return RedirectToAction("Login", "User");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthenticateRequestDto userAuthenticateRequestDto)
        {
            var token = await _userApiService.AuthenticateAsync(userAuthenticateRequestDto);
            _tokenService.SetTokenCookie(token, HttpContext);
            return RedirectToAction("Index", "Fish");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return View();
        }
    }
}
