/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：带数据的基础返回数据对象
*使用说明    ：api返回数据封装
***********************************************************************/

namespace Shared.Infrastructure.Core
{
    public class DataResult : BaseResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        public DataResult()
        {

        }
        public DataResult(int code, string msg, object data) : base(code, msg)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
    }

    public class DataListResult : BaseResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; set; }

        public DataListResult()
        {

        }
        public DataListResult(int code, string msg, object data, int count) : base(code, msg)
        {
            Code = code;
            Msg = msg;
            Data = data;
            Count = count;
        }
    }
}
