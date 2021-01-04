using MediatR;
using Pet.User.Web.Application.User.Commands.Dto;

namespace Pet.User.Web.Application.User.Commands.Command
{
    /// <summary>
    /// 功能描述    ：微信授权，给前端返回用户信息
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public class WechatUserCommand:IRequest<UserAuthDto>
    {
        /// <summary>
        /// openid
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserPhone { get; set; } 
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
    }
}
