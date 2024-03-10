using AutoMapper;
using FishMarket.Domain;
using FishMarket.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Fish, FishDto>().ReverseMap();

            CreateMap<FishCreateDto, FishDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
