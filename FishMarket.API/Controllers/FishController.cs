using AutoMapper;
using FishMarket.Core.Services;
using FishMarket.Domain;
using FishMarket.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Service;

namespace FishMarket.API.Controllers
{
    public class FishController : FMControllerBase
    {
        private readonly IService<Fish, FishDto, FishCreateDto, FishUpdateDto> _fishService;
        protected readonly IMapper _mapper;

        public FishController(IService<Fish, FishDto, FishCreateDto, FishUpdateDto> fishService, IMapper mapper)
        {
            _fishService = fishService;
            _mapper = mapper;
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
            if(fish == null)
            {
                return CreateActionResult(ResponseDto<NoContentDto>.Fail(404, $"Fish with id: {id} not found"));
            }
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
    }
}
