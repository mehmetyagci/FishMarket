using AutoMapper;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FishMarket.Dto.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FishMarket.Service;
using FishMarket.Service.Services;
using FishMarket.API.Filters;

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

        [FMAllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _fishService.GetAllAsync());
        }

        [FMAllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var fish = await _fishService.GetByIdAsync(id);
            return CreateActionResult(fish);
        }

        [FMAllowAnonymous]
        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> CreateAsync([FromForm] FishCreateDto fishCreateDto)
        {
            var result = await _fishService.CreateWithImageAsync(fishCreateDto);

            return CreateActionResult(result);
        }

        [FMAllowAnonymous]
        [HttpPut]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> UpdateAsync([FromForm] FishUpdateDto fishUpdateDto)
        {
            return CreateActionResult(await _fishService.UpdateWithImageAsync(fishUpdateDto));
        }

        [FMAllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return CreateActionResult(await _fishService.DeleteWithImageAsync(id));
        }
    }
}
