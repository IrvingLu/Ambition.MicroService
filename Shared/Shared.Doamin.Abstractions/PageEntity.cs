/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：分页实体基类
*使用说明    ：分页实体基类
***********************************************************************/

namespace Shared.Domain.Abstractions
{

    public abstract class PageEntity
    {
        /// <summary>
        /// 第几个页面
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面显示多少
        /// </summary>
        public int PageSize { get; set; }
    }
}
