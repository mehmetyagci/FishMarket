using AutoMapper;
using FishMarket.API.Authorization;
using FishMarket.API.Controllers;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Service;
using NLayer.Service.Services;

namespace FishMarket.API.Controllers
{
    public class UserController : FMControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
        }

        [FMAllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            return CreateActionResult(await _userService.RegisterAsync(userRegisterDto));
        }

        [FMAllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(UserAuthenticateRequestDto model)
        {
            return CreateActionResult(await _userService.AuthenticateAsync(model));
        }
    }
}
