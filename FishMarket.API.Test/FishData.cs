using FishMarket.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.API.Test
{
    public class FishData
    {
        public static List<FishDto> AllFish { get; set; } = new List<FishDto>
        {
            new FishDto { Id = 1, Name = "Hamsi",  Price = 100.10M, Image = "60c39b56-ef92-4411-96d8-45a33d059f50.jpg" },
            new FishDto { Id = 2, Name = "Levrek", Price = 200.20M, Image = "53df3af8-19e2-4836-b3a2-e6c70ec7ec17.png" },
            new FishDto { Id = 3, Name = "Lüfer",  Price = 300.30M, Image = "611e29ba-b49b-404b-a1da-3ebb9026c5cc.jpg" },
            new FishDto { Id = 4, Name = "Somon",  Price = 400.40M, Image = "150eafe9-360e-4208-9647-2a96a88e964b.jpg" },
        };

        public static List<FishDto> GetTestFishAll()
        {
            return AllFish;
        }

        public static FishDto GetTestFishOne(long id)
        {
            return AllFish.FirstOrDefault(x => x.Id == id);
        }
    }
}
