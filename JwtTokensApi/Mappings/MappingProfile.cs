using AutoMapper;
using JwtTokensApi.Models;
using JwtTokensApi.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RefreshTokenViewModel, RefreshToken>().ReverseMap();

            CreateMap<RoleViewModel, Role>().ReverseMap();
            CreateMap<CreateRoleViewModel, Role>().ReverseMap();
            CreateMap<UpdateRoleViewModel, Role>().ReverseMap();

            CreateMap<UserViewModel, User>().ReverseMap();

            CreateMap<RegisterViewModel, User>().ReverseMap();

            CreateMap<RegisterViewModel, User>().ReverseMap();
        }
    }
}
