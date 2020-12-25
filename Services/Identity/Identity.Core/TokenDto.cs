using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
   public class TokenDto
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据格式
        /// </summary>
        public object Data { get; set; }

        public TokenDto()
        {

        }

        public TokenDto(int code, string message,object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
