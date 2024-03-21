using FishMarket.API.Controllers;
using FishMarket.Core.Services;
using FishMarket.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FishMarket.API.Test
{
    public class UserControllerTest
    {
        #region RegisterAsync
        [Fact]
        public async Task RegisterAsync_Success()
        {
            var mockUserService = new Mock<IUserService>();
            var userRegisterDto = new UserRegisterDto {  
                 Email = "email@email.com",
                 Password = "password",
            };
            var expectedResult = ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
            mockUserService.Setup(service => service.RegisterAsync(userRegisterDto)).ReturnsAsync(expectedResult);

            var controller = new UserController(mockUserService.Object);

            var result = await controller.RegisterAsync(userRegisterDto);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
        #endregion RegisterAsync

        #region AuthenticateAsync
        [Fact]
        public async Task AuthenticateAsync_Success()
        {
            var mockUserService = new Mock<IUserService>();
            var userAuthenticateRequestDto = new UserAuthenticateRequestDto 
            {
                Email = "email@email.com",
                Password = "password",
            };
            var expectedResult = new ResponseDto<UserAuthenticateResponseDto>
            {
                StatusCode = 200,
                Data = new UserAuthenticateResponseDto
                {
                    Token = "token",
                }
            };
            mockUserService.Setup(service => service.AuthenticateAsync(userAuthenticateRequestDto)).ReturnsAsync(expectedResult);

            var controller = new UserController(mockUserService.Object);

            var result = await controller.AuthenticateAsync(userAuthenticateRequestDto);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var responseDto = Assert.IsType<ResponseDto<UserAuthenticateResponseDto>>(objectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, responseDto.StatusCode);
            Assert.NotNull(responseDto.Data.Token);
        }
        #endregion AuthenticateAsync

        #region VerifyEmail
        [Fact]
        public async Task VerifyEmail_Success()
        {
            var mockUserService = new Mock<IUserService>();
            var userVerifyEmailDto = new UserVerifyEmailDto {
                Email = "email@email.com",
                Token = "token",
            };
            var expectedResult = ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
            mockUserService.Setup(service => service.VerifyEmail(userVerifyEmailDto)).ReturnsAsync(expectedResult);

            var controller = new UserController(mockUserService.Object);

            var result = await controller.VerifyEmail(userVerifyEmailDto);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
        #endregion VerifyEmail
    }
}
