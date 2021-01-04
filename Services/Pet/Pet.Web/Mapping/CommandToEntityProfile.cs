using AutoMapper;
using Pet.Web.Application.Pet.Commands.Command;
using System;

namespace Pet.Web.Mapping
{
    public class CommandToEntityProfile: Profile
    {
        public CommandToEntityProfile()
        {
            CreateMap<InsertPetCommand, Domain.PetBase.Pet>().ForMember(f => f.CreateTime, options => options.MapFrom(f => DateTime.Now));
        }
      
    }
}
