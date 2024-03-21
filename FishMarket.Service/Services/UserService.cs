using AutoMapper;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FishMarket.Service.Exceptions;
using FishMarket.Service.Helpers;
using Microsoft.Extensions.Options;

namespace FishMarket.Service.Services
{
    public class UserService : Service<User, UserDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly AppSettings _appSettings;

        private readonly IRepository<User> _repository;

        private readonly IValidator<UserCreateDto> _createValidator;
        private readonly IValidator<UserUpdateDto> _updateValidator;
        private readonly IValidator<UserRegisterDto> _registerValidator;
        public UserService(
            IJwtService jwtService,
            IEmailService emailService,
            IOptions<AppSettings> appSettings,
            IRepository<User> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<UserCreateDto> createValidator,
            IValidator<UserUpdateDto> updateValidator,
            IValidator<UserRegisterDto> registerValidator):
            base(repository, unitOfWork, mapper, createValidator, updateValidator)
        {
            _jwtService = jwtService;
            _emailService = emailService;
            _appSettings = appSettings.Value;
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

            #region Token Creation and Related to user
            var token = Guid.NewGuid().ToString();
            var verificationLink = $"{_appSettings.WebAppURL}verifyemail?email={userRegisterDto.Email}&token={token}";

            await _emailService.SendEmailAsync(new EmailDto
            {
                EmailToId = userRegisterDto.Email,
                EmailToName = userRegisterDto.Email,
                EmailBody = $"Dear {userRegisterDto.Email},<br><br>Please click the link below to confirm your email address:<br><br><a href=\"{verificationLink}\">{verificationLink}</a>",
                EmailSubject = "Confirm Your Email Address",
            });

            newEntity.VerificationToken = token;
            _repository.Update(newEntity);
            await _unitOfWork.CommitAsync();
            #endregion Token Creation and Related to user

            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<ResponseDto<UserAuthenticateResponseDto>> AuthenticateAsync(UserAuthenticateRequestDto userRegisterDto)
        {
            var user = await _repository.FirstOrDefaultAsync(u => u.Email == userRegisterDto.Email);

            if (user == null || user.Password != userRegisterDto.Password)
                throw new NotFoundExcepiton("Username or password is incorrect");

            if (!user.IsEmailVerified)
                throw new ClientSideException("User email is not verified.");

            var response = new UserAuthenticateResponseDto();
            response.Token = _jwtService.GenerateToken(user);
            response.Email = userRegisterDto.Email;
            return ResponseDto<UserAuthenticateResponseDto>.Success(StatusCodes.Status200OK, response);
        }

       
        public async Task<ResponseDto<NoContentDto>> VerifyEmail(UserVerifyEmailDto userVerifyEmailDto)
        {
            var user = await _repository.FirstOrDefaultAsync(u => u.Email == userVerifyEmailDto.Email);

            if (user == null)
                throw new ClientSideException("User not found!");

            if(user.IsEmailVerified)
                throw new ClientSideException("User is already verified!");

            if (user.VerificationToken == userVerifyEmailDto.Token)
            {
                user.IsEmailVerified = true;
                _repository.Update(user);
                await _unitOfWork.CommitAsync();
                return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
            }
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, "Invalid token.");
        }
    }
}
