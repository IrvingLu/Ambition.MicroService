﻿/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：api接口继承
*使用说明    ：api接口
***********************************************************************/

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
        [NonAction]
        public IActionResult Success()
        {
            return Ok(new BaseResult(StatusCodes.Status200OK, "Success"));
        }
        /// <summary>
        /// 成功返回数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Success([ActionResultObjectValue] object data, int? count = null)
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
        [NonAction]
        public IActionResult Error([ActionResultObjectValue] string msg)
        {
            return Ok(new BaseResult(800, msg));
        }
    }
}
