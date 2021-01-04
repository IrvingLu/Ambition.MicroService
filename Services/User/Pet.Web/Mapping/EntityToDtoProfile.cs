using AutoMapper;
using Pet.User.Web.Application.User.Commands.Dto;
using Shared.Domain.Abstractions.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet.User.Web.Mapping
{
    public class EntityToDtoProfile: Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<ApplicationUser, UserInfoDto>();
        }
    }
}
