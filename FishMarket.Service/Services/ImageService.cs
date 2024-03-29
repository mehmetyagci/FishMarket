﻿using FishMarket.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly string _uploadsFolder;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads", "images");
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Invalid image file");

            if (!Directory.Exists(_uploadsFolder))
                Directory.CreateDirectory(_uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(_uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;
        }

        public Task DeleteImageAsync(string imagePath)
        {
            if(imagePath == null || string.IsNullOrEmpty(imagePath))
                throw new ArgumentException("Invalid image path");

            var fullPath = Path.Combine(_uploadsFolder, imagePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.CompletedTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
