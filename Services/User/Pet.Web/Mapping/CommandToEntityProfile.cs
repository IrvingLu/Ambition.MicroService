using AutoMapper;
using Pet.User.Domain.Tenant;
using Pet.User.Domain.User;
using Pet.User.Web.Application.Tenant.Commands.Command;
using Pet.User.Web.Application.User.Commands.Command;
using System;

namespace Pet.User.Web.Mapping
{
    public class CommandToEntityProfile: Profile
    {
        public CommandToEntityProfile()
        {
            CreateMap<CreateServiceCategoryCommand, Tenant_ServiceCategory>().ForMember(f => f.CreateTime, options => options.MapFrom(f => DateTime.Now));
            CreateMap<UpdateTenantInfoCommand, Tenant>().ForMember(f => f.UpdateTime, options => options.MapFrom(f => DateTime.Now));

            CreateMap<AddSuggestCommand, User_Suggest>().ForMember(f => f.CreateTime, options => options.MapFrom(f => DateTime.Now));
        }
      
    }
}
