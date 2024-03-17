using AutoMapper;
using FishMarket.Domain;
using FishMarket.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Service.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            #region User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRegisterDto, User>();

            #endregion User

            #region Fish
            CreateMap<Fish, FishDto>().ReverseMap();
            CreateMap<FishCreateDto, Fish>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<FishUpdateDto, Fish>();
            #endregion Fish
        }
    }
}