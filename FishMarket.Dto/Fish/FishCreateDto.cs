using Microsoft.AspNetCore.Http;


namespace FishMarket.Dto
{
    public class FishCreateDto : BaseDto
    {
        public string Name { get; set; }  
        public decimal Price { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
