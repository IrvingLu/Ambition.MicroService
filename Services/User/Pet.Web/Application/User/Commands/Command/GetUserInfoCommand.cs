using MediatR;
using Pet.User.Web.Application.User.Commands.Dto;

namespace Pet.User.Web.Application.User.Commands.Command
{
    public class GetUserInfoCommand: IRequest<UserInfoDto>
    {
        public string UserId { get; set; }


        public GetUserInfoCommand(string userid)
        {
            UserId = userid;
        }
    }
}
