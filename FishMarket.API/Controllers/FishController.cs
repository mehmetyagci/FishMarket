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

        public FishController(
            IFishService fishService,
            IMapper mapper)
        {
            _fishService = fishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _fishService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var fish = await _fishService.GetByIdAsync(id);
            return CreateActionResult(fish);
        }

        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> CreateAsync([FromForm] FishCreateDto fishCreateDto)
        {
            var result = await _fishService.CreateWithImageAsync(fishCreateDto);

            return CreateActionResult(result);
        }

        [HttpPut]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> UpdateAsync([FromForm] FishUpdateDto fishUpdateDto)
        {
            return CreateActionResult(await _fishService.UpdateWithImageAsync(fishUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return CreateActionResult(await _fishService.DeleteWithImageAsync(id));
        }
    }
}
