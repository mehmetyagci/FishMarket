using FishMarket.Domain;
using FishMarket.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Core.Services
{
    public interface IService<Entity, Dto, CreateDto, UpdateDto> 
        where Entity : BaseEntity 
        where Dto : BaseDto
        where CreateDto : BaseDto
        where UpdateDto : BaseUpdateDto
    {
        Task<ResponseDto<Dto>> GetByIdAsync(long id);
        Task<ResponseDto<IEnumerable<Dto>>> GetAllAsync();
        Task<ResponseDto<Dto>> CreateAsync(CreateDto dto);
        Task<ResponseDto<NoContentDto>> UpdateAsync(UpdateDto dto);
        Task<ResponseDto<NoContentDto>> DeleteAsync(long id);
    }
}
