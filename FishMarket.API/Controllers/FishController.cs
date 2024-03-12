using AutoMapper;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Service;

namespace FishMarket.API.Controllers
{
    public class FishController : FMControllerBase
    {
        private readonly IService<Fish, FishDto, FishCreateDto, FishUpdateDto> _fishService;

        private readonly IValidator<FishCreateDto> _createValidator;
        private readonly IValidator<FishUpdateDto> _updateValidator;

        public FishController(IService<Fish, FishDto, FishCreateDto, FishUpdateDto> fishService, IMapper mapper,
            IValidator<FishCreateDto> createValidator, IValidator<FishUpdateDto> updateValidator)
        {
            _fishService = fishService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
        public async Task<IActionResult> CreateAsync(FishCreateDto fishCreateDto)
        {
            return CreateActionResult(await _fishService.CreateAsync(fishCreateDto));
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
