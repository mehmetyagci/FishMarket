using AutoMapper;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Service;
using NLayer.Service.Services;

namespace FishMarket.API.Controllers
{
    public class FishController : FMControllerBase
    {
        private readonly IFishService _fishService;
        private readonly IImageService _imageService;

        public FishController(
            IFishService fishService,
            IImageService imageService,
            IMapper mapper)
        {
            _fishService = fishService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _fishService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var fish = await _fishService.GetByIdAsync(id);
            return CreateActionResult(fish);
        }

        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> CreateAsync([FromForm] FishCreateDto fishCreateDto)
        {
            var imagePath = await _imageService.SaveImageAsync(fishCreateDto.ImageFile);

            var result = await _fishService.CreateWithImageAsync(fishCreateDto, imagePath);

            return CreateActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(FishUpdateDto fishUpdateDto)
        {
            return CreateActionResult(await _fishService.UpdateAsync(fishUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return CreateActionResult(await _fishService.DeleteAsync(id));
        }
    }
}
