using FishMarket.API.Controllers;
using FishMarket.Core.Services;
using FishMarket.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FishMarket.API.Test
{
    public class FishControllerTest
    {
        #region GetAllAsync
        [Fact]
        public async Task GetAllAsync_Success()
        {
            var expectedFish = FishData.GetTestFishAll();

            var mockFishService = new Mock<IFishService>();

            var mockResult = new ResponseDto<IEnumerable<FishDto>>();
            mockResult.StatusCode = 200;
            mockResult.Data = expectedFish;

            mockFishService.Setup(service => service.GetAllAsync()).ReturnsAsync(mockResult);
            var controller = new FishController(mockFishService.Object);

            var result = await controller.GetAllAsync();

            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsType<ResponseDto<IEnumerable<FishDto>>>(objectResult.Value);
            Assert.Equal(expectedFish, model.Data);
        }
        #endregion GetAllAsync

        #region GetById
        [Fact]
        public async Task GetByIdAsync_Success()
        {
            long id = 1;
            var expectedFish = FishData.GetTestFishOne(id);

            var mockFishService = new Mock<IFishService>();
            var mockResult = new ResponseDto<FishDto>();
            mockResult.StatusCode = 200;
            mockResult.Data = expectedFish;

            mockFishService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(mockResult);
            var controller = new FishController(mockFishService.Object);

            var result = await controller.GetByIdAsync(id);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsType<ResponseDto<FishDto>>(objectResult.Value);
            Assert.Equal(id, model.Data.Id);
        }

        [Fact]
        public async Task GetByIdAsync_Fail()
        {
            long id = -1;
            var mockFishService = new Mock<IFishService>();
            var mockResult = new ResponseDto<FishDto>()
            {
                StatusCode = 400,
                Errors = new List<string> { $"Fish ({id}) not found" }
            };

            mockFishService.Setup(service => service.GetByIdAsync(id)).ReturnsAsync(mockResult);
            var controller = new FishController(mockFishService.Object);

            var result = await controller.GetByIdAsync(id);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsType<ResponseDto<FishDto>>(objectResult.Value);
            Assert.NotNull(model.Errors);
            Assert.True(model.Errors.Count > 0);
        }
        #endregion GetById

        #region CreateWithImageAsync
        [Fact]
        public async Task CreateWithImageAsync_WithImage_Success()
        {
            var mockFishService = new Mock<IFishService>();
            var mockFormFile = new Mock<IFormFile>();
            var fishCreateDto = new FishCreateDto
            {
                Name = "TestFish",
                Price = 10.99M,
                ImageFile = mockFormFile.Object
            };
            var expectedResult = new ResponseDto<FishDto>
            {
                StatusCode = 201,
                Data = new FishDto
                {
                    Id = 1,
                    Name = fishCreateDto.Name,
                    Price = fishCreateDto.Price,
                    Image = $"{Guid.NewGuid().ToString()}.jpg"
                }
            };

            mockFishService.Setup(service => service.CreateWithImageAsync(It.IsAny<FishCreateDto>()))
                           .ReturnsAsync(expectedResult);

            var controller = new FishController(mockFishService.Object);

            var result = await controller.CreateAsync(fishCreateDto);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsType<ResponseDto<FishDto>>(objectResult.Value);
            Assert.Equal(201, model.StatusCode);
            Assert.Equal(expectedResult.Data, model.Data);
        }

        [Fact]
        public async Task CreateWithImageAsync_WithoutImage_Success()
        {
            var mockFishService = new Mock<IFishService>();
            var fishCreateDto = new FishCreateDto
            {
                Name = "TestFish2",
                Price = 99.99M,
            };
            var expectedResult = new ResponseDto<FishDto>
            {
                StatusCode = 201,
                Data = new FishDto
                {
                    Id = 1,
                    Name = fishCreateDto.Name,
                    Price = fishCreateDto.Price,
                }
            };

            mockFishService.Setup(service => service.CreateWithImageAsync(It.IsAny<FishCreateDto>()))
                           .ReturnsAsync(expectedResult);

            var controller = new FishController(mockFishService.Object);

            var result = await controller.CreateAsync(fishCreateDto);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsType<ResponseDto<FishDto>>(objectResult.Value);
            Assert.Equal(201, model.StatusCode);
            Assert.Equal(expectedResult.Data, model.Data);
        }
        #endregion CreateWithImageAsync

        #region UpdateWithImageAsync 
        [Fact]
        public async Task UpdateWithImageAsync_Success()
        {
            var mockFishService = new Mock<IFishService>();
            var mockFormFile = new Mock<IFormFile>();
            var fishUpdateDto = new FishUpdateDto
            {
                Id = 1,
                Name = "UpdatedFish",
                Price = 20.99M,
                ImageFile = mockFormFile.Object
            };
            var expectedResult = ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);

            mockFishService.Setup(service => service.UpdateWithImageAsync(It.IsAny<FishUpdateDto>()))
                           .ReturnsAsync(expectedResult);

            var controller = new FishController(mockFishService.Object);

            var result = await controller.UpdateAsync(fishUpdateDto);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, objectResult.StatusCode);
        }
        #endregion UpdateWithImageAsync 

        #region DeleteAsync
        [Fact]
        public async Task DeleteAsync_Success()
        {
            long id = 1;
            var mockFishService = new Mock<IFishService>();
            var expectedResponse = ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);

            mockFishService.Setup(service => service.DeleteWithImageAsync(id))
                           .ReturnsAsync(expectedResponse);

            var controller = new FishController(mockFishService.Object);

            var result = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
        #endregion DeleteAsync
    }
}