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
        public static List<FishDto> GetTestFishData()
        {
            return new List<FishDto>
            {
                new FishDto { Id = 1, Name = "Hamsi", Price = 100 },
                new FishDto { Id = 2, Name = "Lüfer", Price = 450.50M },
                new FishDto { Id = 2, Name = "Somon", Price = 750.99M },
                new FishDto { Id = 2, Name = "Somon", Price = 750.99M },
            };
        }
    }
}
