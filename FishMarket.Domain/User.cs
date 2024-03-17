using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Domain
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public string VerificationToken { get; set; } = string.Empty;
    }
}
