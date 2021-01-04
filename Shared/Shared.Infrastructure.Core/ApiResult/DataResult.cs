namespace Shared.Infrastructure.Core
{
    public class DataResult : BaseResult
    {
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
        public object Data { get; set; }
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
