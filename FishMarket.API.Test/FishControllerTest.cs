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
            var controller = new FishController(mockFishService.Object);

            var result = await controller.GetAllAsync();

            var objectResult = Assert.IsType<ObjectResult>(result);
            var actualFish = Assert.IsAssignableFrom<ResponseDto<IEnumerable<FishDto>>>(objectResult.Value);
            Assert.Equal(expectedFish, actualFish.Data);
        }
    }
}