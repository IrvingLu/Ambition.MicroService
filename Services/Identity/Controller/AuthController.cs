using IdentityModel.Client;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NMS.Identity.Web.Dto;
using NMS.User.Web.Controllers;
using System.Net.Http;
using System.Threading.Tasks;

namespace NMS.Identity.Web.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthController : BaseController
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginByPwd(PwdLoginDto dto)
        {
            var host = HttpContext.Request.GetDisplayUrl().Split("api")[0];
            var client = new HttpClient();
            var tokenAddress = host + "connect/token";
            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = tokenAddress,
                ClientId = "password",
                ClientSecret = "secret",
                Scope = "api",
                GrantType = "password",
                UserName = dto.UserName,
                Password = dto.Password
            });
            if (response.AccessToken == null)
            {
                return Error(response.ErrorDescription);
            }
            return Success(response.AccessToken);
        }
    }
}
