//using AutoMapper;
//using UserMarket.Core.Services;
//using UserMarket.Domain;
//using UserMarket.Dto;
//using FluentValidation;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using NLayer.Service;

//namespace UserMarket.API.Controllers
//{
//    public class UserController : FMControllerBase
//    {
//        private readonly IService<User, UserDto, UserCreateDto, UserUpdateDto> _userService;

//        private readonly IValidator<UserCreateDto> _createValidator;
//        private readonly IValidator<UserUpdateDto> _updateValidator;

//        public UserController(IService<User, UserDto, UserCreateDto, UserUpdateDto> userService, IMapper mapper,
//            IValidator<UserCreateDto> createValidator, IValidator<UserUpdateDto> updateValidator)
//        {
//            _userService = userService;
//            _createValidator = createValidator;
//            _updateValidator = updateValidator;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllAsync()
//        {
//            return CreateActionResult(await _userService.GetAllAsync());
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetByIdAsync(int id)
//        {
//            var user = await _userService.GetByIdAsync(id);
//            return CreateActionResult(user);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateAsync(UserCreateDto userCreateDto)
//        {
//            return CreateActionResult(await _userService.CreateAsync(userCreateDto));
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateAsync(UserUpdateDto userUpdateDto)
//        {
//            return CreateActionResult(await _userService.UpdateAsync(userUpdateDto));
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            return CreateActionResult(await _userService.DeleteAsync(id));
//        }
//    }
//}
