using Xunit;
using Moq;
using AutoMapper;
using FishMarket.API.Controllers;
using FishMarket.Core.Services;
using FishMarket.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FishMarket.API.Test
{
    public class FishControllerTest
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllFish_Success()
        {
            var expectedFish = FishData.GetTestFishData();

            var mockFishService = new Mock<IFishService>();

            var mockResult = new ResponseDto<IEnumerable<FishDto>>();

            mockResult.StatusCode = 200;
            mockResult.Data = expectedFish;

            mockFishService.Setup(service => service.GetAllAsync()).ReturnsAsync(mockResult);
            var mockMapper = new Mock<IMapper>();
            var controller = new FishController(mockFishService.Object, mockMapper.Object);

            var result = await controller.GetAllAsync();

            var okResult = Assert.IsType<ObjectResult>(result);
            //var actualFish = Assert.IsAssignableFrom<FishDto[]>((okResult.Value as ResponseDto<FishDto>).Data);
            var actualFish = Assert.IsAssignableFrom<IEnumerable<FishDto>>(okResult.Value);
            Assert.Equal(expectedFish, actualFish);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnInternalServerError_Fail()
        {
            var mockFishService = new Mock<IFishService>();
            mockFishService.Setup(service => service.GetAllAsync()).ThrowsAsync(new Exception());
            var mockMapper = new Mock<IMapper>();
            var controller = new FishController(mockFishService.Object, mockMapper.Object);

            var result = await controller.GetAllAsync();

            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = (StatusCodeResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

    }
}