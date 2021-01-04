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
