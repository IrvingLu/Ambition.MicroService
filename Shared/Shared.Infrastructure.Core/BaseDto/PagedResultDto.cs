
/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：分页Dto对象
*使用说明    ：需要分页接口返回的对象
***********************************************************************/

namespace Shared.Infrastructure.Core.BaseDto
{
    public class PagedResultDto
    {
        public int TotalCount { get; set; }

        public object Data { get; set; }

        public PagedResultDto()
        {

        }
        public PagedResultDto(int totalCount, object data)
        { 
            TotalCount = totalCount;
            Data = data;
        }
    }
}
