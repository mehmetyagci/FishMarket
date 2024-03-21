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
        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
