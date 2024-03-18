using AutoMapper;
using FishMarket.Dto;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FishMarket.Web.Controllers
{
    public class FishController : FMWebController
    {
        private readonly FishApiService _fishApiService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private readonly string _apiURL;
        private readonly string _imagePath;


        public FishController(FishApiService fishApiService, IMapper mapper, IConfiguration configuration)
        {
            _fishApiService = fishApiService;
            _mapper = mapper;
            _configuration = configuration;
            _apiURL = _configuration["API:URL"];
            _imagePath = _configuration["API:ImagePath"];
        }

        public async Task<IActionResult> Index()
        {
            var response = await _fishApiService.GetAllAsync();
            ViewBag.ImagePath = $"{_apiURL}{_imagePath}";
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FishCreateDto fishCreateDto)
        {
            if (ModelState.IsValid)
            {
                await _fishApiService.CreateAsync(fishCreateDto);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // [ServiceFilter(typeof(NotFoundFilter<Fish>))]
        public async Task<IActionResult> Update(int id)
        {
            var fishDto = await _fishApiService.GetByIdAsync(id);
            var fishUpdateDto = _mapper.Map<FishUpdateDto>(fishDto);
            return View(fishUpdateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FishUpdateDto fishUpdateDto)
        {
            if (ModelState.IsValid)
            {
                await _fishApiService.UpdateAsync(fishUpdateDto);
                return RedirectToAction(nameof(Index));
            }
            return View(fishUpdateDto);
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _fishApiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
