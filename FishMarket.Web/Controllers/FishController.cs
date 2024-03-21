using AutoMapper;
using FishMarket.Dto;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Web.Controllers
{
    public class FishController : FMWebController
    {
        private readonly FishApiService _fishApiService;
        private readonly IMapper _mapper;

        public FishController(FishApiService fishApiService, IMapper mapper, IConfiguration configuration)
            : base(configuration)
        {
            _fishApiService = fishApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _fishApiService.GetAllAsync();
            ViewBag.ImagePath = $"{_apiURL}{_imagePath}";
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FishCreateDto fishCreateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _fishApiService.CreateAsync(fishCreateDto);
                if (result == null)
                {
                    ModelState.AddModelError("", "Failed to create fish. Please try again.");
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

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
                var result = await _fishApiService.UpdateAsync(fishUpdateDto);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update fish. Please try again.");
                }
            }
            return View(fishUpdateDto);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var result = await _fishApiService.DeleteAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete fish. Please try again.");
                return View();
            }
        }
    }
}
