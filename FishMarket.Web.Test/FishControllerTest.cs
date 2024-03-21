using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FishMarket.Dto;
using FishMarket.Service.Helpers;
using FishMarket.Web.Controllers;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace FishMarket.Web.Test
{
    public class FishControllerTest
    {
        private readonly FishController _controller;
        private readonly Mock<IFishApiService> _mockFishApiService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IConfiguration _configuration;

        public FishControllerTest()
        {
            _mockFishApiService = new Mock<IFishApiService>();
            _mockMapper = new Mock<IMapper>();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                {"API:URL", "http://example.com/api"},  
                {"API:ImagePath", "/images"},  
                })
                .Build();

            _configuration = configuration;
            _controller = new FishController(_mockFishApiService.Object, _mockMapper.Object, _configuration);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfFishDto()
        {
            var fishList = FishData.GetTestFishAll();
            _mockFishApiService.Setup(service => service.GetAllAsync()).ReturnsAsync(fishList);

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(fishList, viewResult.Model);
        }

        #region Create
        [Fact]
        public async Task Create_ReturnsViewResult()
        {
            var result = await _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_WithValidModelStateAndImage_ReturnsRedirectToActionResult()
        {
            var imageStream = new MemoryStream();  
            var imageFileMock = new Mock<IFormFile>();
            imageFileMock.SetupGet(f => f.FileName).Returns("test.jpg");
            imageFileMock.SetupGet(f => f.Length).Returns(imageStream.Length);
            imageFileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask);

            _mockFishApiService.Setup(service => service.CreateAsync(It.IsAny<FishCreateDto>())).ReturnsAsync(new FishDto());

            var result = await _controller.Create(new FishCreateDto
            {
                Name = "Test Fish",
                Price = 10.50M,
                ImageFile = imageFileMock.Object
            }) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal(nameof(FishController.Index), result.ActionName);
            Assert.Null(result.ControllerName);  
        }
        #endregion Create

        #region Update
        [Fact]
        public async Task Update_Get_ReturnsViewResultWithFishUpdateDto()
        {
            int fishId = 1;
            var fishDto = new FishDto { Id = fishId, Name = "Test Fish", Price = 10.50M };
            var fishUpdateDto = new FishUpdateDto { Id = fishId, Name = "Test Fish", Price = 10.50M };
            _mockFishApiService.Setup(service => service.GetByIdAsync(fishId)).ReturnsAsync(fishDto);
            _mockMapper.Setup(mapper => mapper.Map<FishUpdateDto>(fishDto)).Returns(fishUpdateDto);

            var result = await _controller.Update(fishId);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<FishUpdateDto>(viewResult.Model);
            Assert.Equal(fishUpdateDto, viewResult.Model);
        }

        [Fact]
        public async Task Update_Post_WithValidModel_ReturnsRedirectToActionResult()
        {
            var fishUpdateDto = new FishUpdateDto { Id = 1, Name = "Updated Fish", Price = 15.75M };
            _mockFishApiService.Setup(service => service.UpdateAsync(fishUpdateDto)).ReturnsAsync(true);

            var result = await _controller.Update(fishUpdateDto);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Update_Post_WithInvalidModel_ReturnsViewResultWithFishUpdateDto()
        {
            var fishUpdateDto = new FishUpdateDto { Id = 1, Name = "Updated Fish", Price = 15.75M };
            _controller.ModelState.AddModelError("Name", "Name is required");

            var result = await _controller.Update(fishUpdateDto);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<FishUpdateDto>(viewResult.Model);
            Assert.Equal(fishUpdateDto, viewResult.Model);
        }
        #endregion Update

        #region Delete
        [Fact]
        public async Task Delete_WithValidId_ReturnsRedirectToActionResult()
        {
            long fishId = 1;
            _mockFishApiService.Setup(service => service.DeleteAsync(fishId)).ReturnsAsync(true);

            var result = await _controller.Delete(fishId);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsViewResultWithModelStateError()
        {
            long fishId = 1;
            _mockFishApiService.Setup(service => service.DeleteAsync(fishId)).ReturnsAsync(false);

            var result = await _controller.Delete(fishId);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
            Assert.Equal("Failed to delete fish. Please try again.", viewResult.ViewData.ModelState[""].Errors[0].ErrorMessage);
        }
        #endregion Delete
    }
}
