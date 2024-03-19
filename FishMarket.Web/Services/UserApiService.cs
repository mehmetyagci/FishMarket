using FishMarket.Dto;
using System.Net.Http.Json;

namespace FishMarket.Web.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiHeader = "User/";
        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{apiHeader}register", userRegisterDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VerifyEmail(UserVerifyEmailDto userVerifyEmailDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{apiHeader}authenticate", userVerifyEmailDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> AuthenticateAsync(UserAuthenticateRequestDto userAuthenticateRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{apiHeader}authenticate", userAuthenticateRequestDto);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<ResponseDto<UserAuthenticateResponseDto>>();
            return responseBody.Data.Token;
        }

      
    }
}
