using Microsoft.AspNetCore.Mvc;
using Pet.User.Web.Application.Wechat;
using Shared.Infrastructure.Core;
using System.Net;

namespace Pet.User.Web.Controllers
{
    /// <summary>
    /// 功能描述    ：微信相关接口
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WechatController : BaseController
    {
        private readonly IWechatService _wechatService;
        public WechatController(IWechatService wechatService)
        {
            _wechatService = wechatService;
        }
        #region Methods
        /// <summary>
        /// 获取电话号
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("phone")]
        public IActionResult GetPhoneNumberAsync([FromBody] GetPhoneDto dto)
        {
            var result = _wechatService.GetPhoneNumber(dto.EncryptedData, dto.IV, dto.Session_key);
            return Ok(new DataResult((int)HttpStatusCode.OK, "Success", result));
        }
        /// <summary>
        /// 获取小程序基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("openInfo")]
        public IActionResult GetOpenInfoAsync([FromQuery] string code)
        {
            var result = _wechatService.GetOpenInfoAsync(code);
            return Ok(new DataResult((int)HttpStatusCode.OK, "Success", result));
        }
        #endregion
    }
    public class GetPhoneDto
    {
        public string EncryptedData { get; set; }
        public string IV { get; set; }
        public string Session_key { get; set; }
    }
}
