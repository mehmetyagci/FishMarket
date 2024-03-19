using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Web.Controllers
{
    public class FMWebController : Controller
    {
        protected readonly IConfiguration _configuration;
        protected readonly string _apiURL;
        protected readonly string _imagePath;

        public FMWebController(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiURL = _configuration["API:URL"] ?? string.Empty;
            _imagePath = _configuration["API:ImagePath"] ?? string.Empty;
        }
    }
}
