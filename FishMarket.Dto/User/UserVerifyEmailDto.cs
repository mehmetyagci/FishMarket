using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Dto
{
    public class UserVerifyEmailDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
