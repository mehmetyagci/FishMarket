using Microsoft.AspNetCore.Http;

namespace FishMarket.Core.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
