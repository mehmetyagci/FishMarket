using FishMarket.Dto;
using System.Net.Http.Json;

namespace FishMarket.Web.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _apiRoot = "User/";
        public UserApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("FishApiHttpClient");
            _apiBaseUrl = configuration["API:Url"] + "api/";
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
        }

        public async Task<bool> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiRoot}register", userRegisterDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VerifyEmail(UserVerifyEmailDto userVerifyEmailDto)
        {
            var response = await _httpClient.GetAsync($"{_apiRoot}verifyemail?email={userVerifyEmailDto.Email}&token={userVerifyEmailDto.Token}");
            return response.IsSuccessStatusCode;
        }

        public async Task<UserAuthenticateResponseDto> AuthenticateAsync(UserAuthenticateRequestDto userAuthenticateRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiRoot}authenticate", userAuthenticateRequestDto);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<ResponseDto<UserAuthenticateResponseDto>>();
            return responseBody.Data;
        }

      
    }
}
