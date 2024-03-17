using FishMarket.Domain;
using FishMarket.Dto;

namespace FishMarket.Core.Services
{
    public interface IFishService : IService<Fish, FishDto, FishCreateDto, FishUpdateDto>
    {
        Task<ResponseDto<FishDto>> CreateWithImageAsync(FishCreateDto fishCreateDto);
        Task<ResponseDto<NoContentDto>> UpdateWithImageAsync(FishUpdateDto fishUpdateDto);
        Task<ResponseDto<NoContentDto>> DeleteWithImageAsync(long id);
    }
}