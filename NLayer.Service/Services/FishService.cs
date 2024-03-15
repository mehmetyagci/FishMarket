using AutoMapper;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NLayer.Service.Services
{
    public class FishService : Service<Fish, FishDto, FishCreateDto, FishUpdateDto>, IFishService
    {
        private readonly IRepository<Fish> _repository;

        private readonly IValidator<FishCreateDto> _createValidator;
        private readonly IValidator<FishUpdateDto> _updateValidator;
        public FishService(
            IRepository<Fish> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<FishCreateDto> createValidator,
            IValidator<FishUpdateDto> updateValidator) :
            base(repository, unitOfWork, mapper, createValidator, updateValidator)
        {
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ResponseDto<FishDto>> CreateWithImageAsync(FishCreateDto fishCreateDto, string imagePath)
        {
            await _createValidator.ValidateAndThrowAsync(fishCreateDto);

            Fish newEntity = _mapper.Map<Fish>(fishCreateDto);
            newEntity.Image = imagePath;
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<FishDto>(newEntity);
            return ResponseDto<FishDto>.Success(StatusCodes.Status200OK, newDto);
        }
    }
}
