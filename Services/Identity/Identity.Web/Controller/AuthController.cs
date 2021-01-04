using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Core;
using Shared.Infrastructure.Core.Tools;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Identity.Web.Controller
{
    /// <summary>
    /// 功能描述    ：授权认证接口
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:48:53 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:48:53 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// 登录授权封装
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AuthAsync([FromBody] LoginDto request)
        {
            string str = "http://localhost:" + Request.HttpContext.Connection.LocalPort;
            //对密码进行解密
            var password =RSA2Helper.Decrypt(request.Password, Encoding.Default, RSAConfig.PublicKey, RSAConfig.PrivateKey);
            ///验证请求
            var result = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = str + "/connect/token",
                ClientId = "client",
                ClientSecret = "secret",
                GrantType= "password",
                //Scope = "api",
                UserName = request.UserName,
                Password = password
            });
            ///重新封装数据
            if (result.AccessToken == null)
            {
                ///登录失败,具体原因参照Message
                return Ok(new DataResult { Code = (int)HttpStatusCode.InternalServerError, Msg = result.ErrorDescription});
            }
            ///登录成功返回Token
            return Ok(new DataResult { Code = (int)HttpStatusCode.OK, Msg = "验证通过", Data = result.AccessToken });

        }
    }

    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
