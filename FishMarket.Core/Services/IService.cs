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
    public interface IService<Entity, Dto> where Entity : BaseEntity where Dto : BaseDto
    {
        Task<ResponseDto<Dto>> GetByIdAsync(long id);
        Task<ResponseDto<IEnumerable<Dto>>> GetAllAsync();
        Task<ResponseDto<Dto>> AddAsync(Dto dto);
        Task<ResponseDto<NoContentDto>> UpdateAsync(Dto dto);
        Task<ResponseDto<NoContentDto>> DeleteAsync(long id);
    }
}
