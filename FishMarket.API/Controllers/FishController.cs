using AutoMapper;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Service;

namespace FishMarket.API.Controllers
{
    public class FishController : FMControllerBase
    {
        private readonly IService<Fish, FishDto, FishCreateDto, FishUpdateDto> _service;
        private readonly IImageService _imageService;

        private readonly IValidator<FishCreateDto> _createValidator;
        private readonly IValidator<FishUpdateDto> _updateValidator;

        public FishController(
            IService<Fish, FishDto, FishCreateDto, FishUpdateDto> service,
            IImageService imageService,
            IMapper mapper,
            IValidator<FishCreateDto> createValidator,
            IValidator<FishUpdateDto> updateValidator)
        {
            _service = service;
            _imageService = imageService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var fish = await _service.GetByIdAsync(id);
            return CreateActionResult(fish);
        }

        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> CreateAsync([FromForm] FishCreateDto fishCreateDto)
        {
            var imagePath = await _imageService.SaveImageAsync(fishCreateDto.ImageFile);

            var result = await _service.CreateAsync(fishCreateDto);

            return CreateActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(FishUpdateDto fishUpdateDto)
        {
            return CreateActionResult(await _service.UpdateAsync(fishUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return CreateActionResult(await _service.DeleteAsync(id));
        }
    }
}
