/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：基础返回数据对象
*使用说明    ：api返回数据封装
***********************************************************************/

namespace Shared.Infrastructure.Core
{
    public class BaseResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        public BaseResult()
        {

        }
        public BaseResult(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }
    }
}
