using AutoMapper;
using FishMarket.Core;
using FishMarket.Core.Repositories;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NLayer.Service.Exceptions;

namespace NLayer.Service.Services
{
    public class FishService : Service<Fish, FishDto, FishCreateDto, FishUpdateDto>, IFishService
    {
        private readonly IImageService _imageService;

        private readonly IRepository<Fish> _repository;

        private readonly IValidator<FishCreateDto> _createValidator;
        private readonly IValidator<FishUpdateDto> _updateValidator;
        public FishService(
            IImageService imageService,
            IRepository<Fish> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<FishCreateDto> createValidator,
            IValidator<FishUpdateDto> updateValidator) :
            base(repository, unitOfWork, mapper, createValidator, updateValidator)
        {
            _imageService = imageService;
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ResponseDto<FishDto>> CreateWithImageAsync(FishCreateDto fishCreateDto)
        {
            await _createValidator.ValidateAndThrowAsync(fishCreateDto);

            string imagePath = string.Empty;
            if (fishCreateDto.ImageFile != null) 
                 imagePath = await _imageService.SaveImageAsync(fishCreateDto.ImageFile);

            Fish newEntity = _mapper.Map<Fish>(fishCreateDto);
            newEntity.Image = imagePath;
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<FishDto>(newEntity);
            return ResponseDto<FishDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateWithImageAsync(FishUpdateDto fishUpdateDto)
        {
            await _updateValidator.ValidateAndThrowAsync(fishUpdateDto);

            var checkEntity = await _repository.GetByIdAsync(fishUpdateDto.Id);
            if (checkEntity == null)
            {
                throw new NotFoundExcepiton($"{typeof(Fish).Name}({fishUpdateDto.Id}) not found");
            }
            _repository.Detach(checkEntity);

            if(!string.IsNullOrEmpty(checkEntity.Image)) 
                await _imageService.DeleteImageAsync(checkEntity.Image);

            string newImagePath = string.Empty;
            if(fishUpdateDto.ImageFile != null )
                newImagePath = await _imageService.SaveImageAsync(fishUpdateDto.ImageFile);

            var entity = _mapper.Map<Fish>(fishUpdateDto);
            entity.Image = newImagePath;
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteWithImageAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundExcepiton($"{typeof(Fish).Name}({id}) not found");
            }

            if(!string.IsNullOrEmpty(entity.Image))
                await _imageService.DeleteImageAsync(entity.Image);

            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
