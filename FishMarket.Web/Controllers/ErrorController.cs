using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FishMarket.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var exceptionData = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Message = "There is an exception!";
            _logger.LogError("Exception Url: {url}, Exception Message: {message}",
                              exceptionData?.Path, exceptionData?.Error.Message);

            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.Message = "Page Not Found!";
                    break;
                case 500:
                    ViewBag.Message = "Internal Server Error!";
                    break;
                default:
                    ViewBag.Message = "There is an exception!";
                    break;

            }
            _logger.LogError("Status Code:{statusCode}, Request Path: {path}",
                                            statusCode, statusCodeData.OriginalPath);

            return View();
        }
    }
}
