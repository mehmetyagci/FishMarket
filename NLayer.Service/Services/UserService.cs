using AutoMapper;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NLayer.Service.Exceptions;

namespace NLayer.Service.Services
{
    public class UserService : Service<User, UserDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IJWTService _jwtService;
        //private readonly IEmailService _emailService;

        private readonly IRepository<User> _repository;

        private readonly IValidator<UserCreateDto> _createValidator;
        private readonly IValidator<UserUpdateDto> _updateValidator;
        private readonly IValidator<UserRegisterDto> _registerValidator;
        public UserService(
            IJWTService jwtService,
          //  IEmailService emailService,
            IRepository<User> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<UserCreateDto> createValidator,
            IValidator<UserUpdateDto> updateValidator,
            IValidator<UserRegisterDto> registerValidator):
            base(repository, unitOfWork, mapper, createValidator, updateValidator)
        {
            _jwtService = jwtService;
            //_emailService = emailService;
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _registerValidator = registerValidator;
        }

        public  async Task<ResponseDto<NoContentDto>> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            await _registerValidator.ValidateAndThrowAsync(userRegisterDto);

            var hashPassword = userRegisterDto.Password; // TODO: will remove BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            User newEntity = _mapper.Map<User>(userRegisterDto);
            newEntity.Password = hashPassword;
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<UserDto>(newEntity);

            //await _emailService.SendEmailAsync(userRegisterDto.Email, "subject", "body");

            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<ResponseDto<UserAuthenticateResponseDto>> AuthenticateAsync(UserAuthenticateRequestDto userRegisterDto)
        {
            var user = await _repository.FirstOrDefaultAsync(u => u.Email == userRegisterDto.Email);

            if (user == null || user.Password != userRegisterDto.Password)
                throw new NotFoundExcepiton("Username or password is incorrect");

            var response = new UserAuthenticateResponseDto();
            response.Token = _jwtService.GenerateToken(user);
            return ResponseDto<UserAuthenticateResponseDto>.Success(StatusCodes.Status200OK, response);
        }
    }
}
