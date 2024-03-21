using FishMarket.Dto;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FishMarket.Web.Services
{
    public class FishApiService : IFishApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _apiBaseUrl;
        private readonly string _apiRoot = "Fish/";

        public FishApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) 
        {
            _httpClient = httpClientFactory.CreateClient("FishApiHttpClient");
            _apiBaseUrl = configuration["API:Url"] + "api/";
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtTokenToRequest()
        {
            var jwtToken = _httpContextAccessor.HttpContext.Request.Cookies["JwtToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        public async Task<List<FishDto>> GetAllAsync()
        {
            AddJwtTokenToRequest();
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<FishDto>>>(_apiRoot);
            return response.Data;
        }

        public async Task<FishDto> GetByIdAsync(long id)
        {
            AddJwtTokenToRequest();
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<FishDto>>($"{_apiRoot}{id}");
            return response.Data;
        }

        public async Task<FishDto> CreateAsync(FishCreateDto fishCreateDto)
        {
            AddJwtTokenToRequest();
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(fishCreateDto.Name), "Name");
            formData.Add(new StringContent(fishCreateDto.Price.ToString()), "Price");
            if (fishCreateDto.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await fishCreateDto.ImageFile.CopyToAsync(memoryStream);
                    var imageContent = new ByteArrayContent(memoryStream.ToArray());
                    formData.Add(imageContent, "ImageFile", fishCreateDto.ImageFile.FileName);
                }
            }

            var response = await _httpClient.PostAsync(_apiRoot, formData);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<ResponseDto<FishDto>>();
            return responseBody.Data;
        }
        public async Task<bool> UpdateAsync(FishUpdateDto fistUpdateDto)
        {
            AddJwtTokenToRequest();
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(fistUpdateDto.Id.ToString()), "Id");
            formData.Add(new StringContent(fistUpdateDto.Name), "Name");
            formData.Add(new StringContent(fistUpdateDto.Price.ToString()), "Price");
            if (fistUpdateDto.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await fistUpdateDto.ImageFile.CopyToAsync(memoryStream);
                    var imageContent = new ByteArrayContent(memoryStream.ToArray());
                    formData.Add(imageContent, "ImageFile", fistUpdateDto.ImageFile.FileName);
                }
            }

            var response = await _httpClient.PutAsync(_apiRoot, formData);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            AddJwtTokenToRequest();
            var response = await _httpClient.DeleteAsync($"{_apiRoot}{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
