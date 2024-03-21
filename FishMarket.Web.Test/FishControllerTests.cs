using Moq;
using Xunit;

namespace FishMarket.Web.Test
{
    public class FishControllerTests
    {
        //[Fact]
        //public async Task Index_ReturnsViewResult_WithModel()
        //{
        //    var mockFishApiService = new Mock<FishApiService>(MockBehavior.Strict, null);
        //    var fishList = FishData.GetTestFishAll(); // Use the test fish data
        //    mockFishApiService.Setup(service => service.GetAllAsync()).ReturnsAsync(fishList);
        //    var controller = new FishController(mockFishApiService.Object, null, null);

        //    var result = await controller.Index();

        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<FishDto>>(viewResult.Model);
        //    Assert.Equal(fishList.Count(), model.Count());
        //    Assert.Equal("https://your-api-url.com/your-image-path/", controller.ViewBag.ImagePath);
        //    mockFishApiService.Verify(service => service.GetAllAsync(), Times.Once);
        //}
    }
}