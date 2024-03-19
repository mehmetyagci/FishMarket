using FishMarket.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Core.Services
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
        public long? ValidateToken(string token);
    }
}
