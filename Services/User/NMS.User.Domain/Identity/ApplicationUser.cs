using Microsoft.AspNetCore.Identity;

namespace NMS.User.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 用户头像
        /// </summary>
       public string Avatar { get; set; }
    }
}
