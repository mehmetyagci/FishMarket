using FishMarket.Service.Filters;
using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FMControllerBase : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
