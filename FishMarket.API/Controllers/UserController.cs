﻿using AutoMapper;
using FishMarket.API.Filters;
using FishMarket.Core.Services;
using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;

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
