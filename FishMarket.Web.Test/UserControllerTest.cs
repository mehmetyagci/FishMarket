using System.Collections.Generic;
using System.Threading.Tasks;
using FishMarket.Dto;
using FishMarket.Web.Controllers;
using FishMarket.Web.Helpers;
using FishMarket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace FishMarket.Web.Test
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserApiService> _mockUserApiService;
        private readonly Mock<TokenService> _mockTokenService;
        private readonly IConfiguration _configuration;

        public UserControllerTests()
        {
            _mockUserApiService = new Mock<IUserApiService>();
            _mockTokenService = new Mock<TokenService>();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"API:URL", "http://example.com/api"},
                    {"API:ImagePath", "/images"},
                })
                .Build();

            _configuration = configuration;
            _controller = new UserController(_mockUserApiService.Object, _configuration, _mockTokenService.Object);

            //var httpContext = new DefaultHttpContext();
            //httpContext.Request.Headers["Cookie"] = "JwtToken=testToken";
            //var controllerContext = new ControllerContext()
            //{
            //    HttpContext = httpContext,
            //};

            //_controller = new UserController(_mockUserApiService.Object, _configuration, _mockTokenService.Object)
            //{
            //    ControllerContext = controllerContext
            //};
        }

        [Fact]
        public async Task Register_ReturnsViewResult()
        {
            var result = await _controller.Register();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Register_ValidModel_RedirectsToHomeIndex()
        {
            var userRegisterDto = new UserRegisterDto();
            _mockUserApiService.Setup(service => service.RegisterAsync(userRegisterDto)).ReturnsAsync(true);

            var result = await _controller.Register(userRegisterDto);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task VerifyEmailGet_ReturnsViewResult()
        {
            // Arrange
            var userVerifyEmailDto = new UserVerifyEmailDto();

            // Act
            var actionResult = await _controller.VerifyEmailGet(userVerifyEmailDto);

            // Assert
            var result = Assert.IsType<ViewResult>(actionResult);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task VerifyEmailPost_WithValidModel_ReturnsRedirectToActionResult()
        {
            var userVerifyEmailDto = new UserVerifyEmailDto();
            _mockUserApiService.Setup(service => service.VerifyEmail(userVerifyEmailDto)).ReturnsAsync(true);

            var result = await _controller.VerifyEmail(userVerifyEmailDto);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
            Assert.Equal("User", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Login_ReturnsViewResult()
        {
            var result = await _controller.Login();

            Assert.IsType<ViewResult>(result);
        }

       

        [Fact]
        public async Task Logout_ReturnsRedirectToActionResult()
        {
            var result = await _controller.Logout();

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }
    }
}
