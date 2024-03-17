using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Dto
{
    public class UserAuthenticateResponseDto
    {
        public string Token { get; set; } = string.Empty;
    }
}
