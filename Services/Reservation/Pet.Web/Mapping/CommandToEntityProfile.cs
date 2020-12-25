using AutoMapper;
using Pet.Reservation.Web.Application.Commands.Command;
using System;

namespace Pet.Reservation.Web.Mapping
{
    /// <summary>
    /// 功能描述 ：命令到dto的映射
    /// 创 建 者 ：鲁岩奇
    /// 创建日期 ：2020/12/25 17:07:59 
    /// </summary>
    public class CommandToEntityProfile : Profile
    {
        public CommandToEntityProfile()
        {
            CreateMap<CreateReservationCommand, Domain.Reservation.Reservation>().ForMember(f => f.CreateTime, options => options.MapFrom(f => DateTime.Now));
        }
    }
}
