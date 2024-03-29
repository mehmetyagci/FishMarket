﻿using FishMarket.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Core.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailDto emailDto);
    }
}
