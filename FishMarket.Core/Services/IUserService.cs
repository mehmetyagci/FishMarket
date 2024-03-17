using FishMarket.Domain;
using FishMarket.Dto;

namespace FishMarket.Core.Services
{
    public interface IUserService : IService<User, UserDto, UserCreateDto, UserUpdateDto>
    {
        Task<ResponseDto<NoContentDto>> RegisterAsync(UserRegisterDto userRegisterDto);

        Task<ResponseDto<UserAuthenticateResponseDto>> AuthenticateAsync(UserAuthenticateRequestDto userAuthenticateRequestDto);
    }
}