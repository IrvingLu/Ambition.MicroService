using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shared.Infrastructure.Core;

namespace NMS.User.Web.Controllers
{
    /// <summary>
    /// 自定义状态码
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ObjectResult Success()
        {
            return Ok(new BaseResult(StatusCodes.Status200OK, "Success"));
        }
        /// <summary>
        /// 成功返回数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ObjectResult Success([ActionResultObjectValue] object data, int? count = null)
        {
            return count != null
                ? Ok(new DataListResult(StatusCodes.Status200OK, "Success", data, (int)count))
                : Ok(new DataResult(StatusCodes.Status200OK, "Success", data));

        }
        /// <summary>
        /// 内部逻辑可识别错误，返回统一状态码，前端拦截
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ObjectResult Error([ActionResultObjectValue] string msg, object data)
        {
            return Ok(new DataResult(500, msg, data));
        }
    }
}
