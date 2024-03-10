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
        private readonly IService<FishMarket.Domain.Fish, FishMarket.Dto.FishDto> _fishService;
        protected readonly IMapper _mapper;

        public FishController(IService<FishMarket.Domain.Fish, FishMarket.Dto.FishDto> fishService, IMapper mapper)
        {
            _fishService = fishService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CreateActionResult(await _fishService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Save(FishCreateDto fishCreateDto)
        {
            var fishDto = _mapper.Map<FishDto>(fishCreateDto);
            return CreateActionResult(await _fishService.AddAsync(fishDto));
        }
    }
}
