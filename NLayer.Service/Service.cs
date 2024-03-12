using AutoMapper;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace NLayer.Service
{
    public class Service<Entity, Dto, CreateDto, UpdateDto> : IService<Entity, Dto, CreateDto, UpdateDto> 
        where Entity : BaseEntity 
        where Dto : BaseDto
        where CreateDto : BaseDto
        where UpdateDto : BaseDto
    {
        protected readonly IRepository<Entity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public Service(IRepository<Entity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<Dto>> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = _mapper.Map<Dto>(entity);
            return ResponseDto<Dto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ResponseDto<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();
            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);
            return ResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<ResponseDto<Dto>> CreateAsync(CreateDto dto)
        {
            Entity newEntity = _mapper.Map<Entity>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<Dto>(newEntity);
            return ResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateAsync(UpdateDto dto)
        {
            var entity = _mapper.Map<Entity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}