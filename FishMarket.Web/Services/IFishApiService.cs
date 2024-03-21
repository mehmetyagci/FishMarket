using FishMarket.Dto;

namespace FishMarket.Web.Services
{
    public interface IFishApiService
    {
        Task<List<FishDto>> GetAllAsync();
        Task<FishDto> GetByIdAsync(long id);
        Task<FishDto> CreateAsync(FishCreateDto fishCreateDto);
        Task<bool> UpdateAsync(FishUpdateDto fistUpdateDto);
        Task<bool> DeleteAsync(long id);
    }
}
