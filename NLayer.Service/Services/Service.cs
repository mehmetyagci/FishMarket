using AutoMapper;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using NLayer.Service.Exceptions;
using FluentValidation;

namespace NLayer.Service.Services
{
    public class Service<Entity, Dto, CreateDto, UpdateDto> : IService<Entity, Dto, CreateDto, UpdateDto>
        where Entity : BaseEntity
        where Dto : BaseDto
        where CreateDto : BaseDto
        where UpdateDto : BaseUpdateDto
    {
        private readonly IRepository<Entity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        private readonly IValidator<CreateDto> _createValidator;
        private readonly IValidator<UpdateDto> _updateValidator;

        public Service(
            IRepository<Entity> repository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IValidator<CreateDto> createValidator, 
            IValidator<UpdateDto> updateValidator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ResponseDto<Dto>> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundExcepiton($"{typeof(Entity).Name}({id}) not found");
            }
            var dto = _mapper.Map<Dto>(entity);
            return ResponseDto<Dto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ResponseDto<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();
            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);
            return ResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<ResponseDto<Dto>> CreateAsync(CreateDto createDto)
        {
            await _createValidator.ValidateAndThrowAsync(createDto);

            Entity newEntity = _mapper.Map<Entity>(createDto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<Dto>(newEntity);
            return ResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateAsync(UpdateDto updateDto)
        {
            await _updateValidator.ValidateAndThrowAsync(updateDto);

            var checkEntity = await _repository.GetByIdAsync(updateDto.Id);
            if (checkEntity == null)
            {
                throw new NotFoundExcepiton($"{typeof(Entity).Name}({updateDto.Id}) not found");
            }
            _repository.Detach(checkEntity);

            var entity = _mapper.Map<Entity>(updateDto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundExcepiton($"{typeof(Entity).Name}({id}) not found");
            }
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}