using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Dto
{
    public static class Helper
    {
        public static bool CheckValidImage(IFormFile imageFile)
        {
            if (imageFile == null) return true;
            if (imageFile.Length == 0) return false;

            const int maxFileSizeInBytes = 5 * 1024 * 1024;
            if (imageFile.Length > maxFileSizeInBytes) return false;

            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
