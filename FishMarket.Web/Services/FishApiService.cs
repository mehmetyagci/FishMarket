using FishMarket.Dto;

namespace FishMarket.Web.Services
{
    public class FishApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string apiHeader = "Fish/";

        public FishApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FishDto>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<FishDto>>>(apiHeader);
            return response.Data;
        }

        public async Task<FishDto> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<FishDto>>($"{apiHeader}/{id}");
            return response.Data;
        }

        public async Task<FishDto> CreateAsync(FishCreateDto fishCreateDto)
        {
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

            var response = await _httpClient.PostAsync("Fish", formData);
            if (!response.IsSuccessStatusCode) return null;
            var responseBody = await response.Content.ReadFromJsonAsync<ResponseDto<FishDto>>();
            return responseBody.Data;
        }
        public async Task<bool> UpdateAsync(FishUpdateDto fistUpdateDto)
        {
            var formData = new MultipartFormDataContent();
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

            var response = await _httpClient.PutAsync(apiHeader, formData);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"{apiHeader}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
