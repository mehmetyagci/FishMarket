using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Dto
{
    public class FishUpdateDto : BaseUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
