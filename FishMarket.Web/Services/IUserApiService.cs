using FishMarket.Dto;

namespace FishMarket.Web.Services
{
    public interface IUserApiService
    {
        Task<bool> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<bool> VerifyEmail(UserVerifyEmailDto userVerifyEmailDto);
        Task<UserAuthenticateResponseDto> AuthenticateAsync(UserAuthenticateRequestDto userAuthenticateRequestDto);
    }
}
