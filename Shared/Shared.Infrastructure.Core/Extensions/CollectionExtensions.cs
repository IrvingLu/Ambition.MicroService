/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：集合扩展
*使用说明    ：集合扩展
***********************************************************************/

using System.Collections.Generic;

namespace NMS.Reservation.Web.Core.Extensions
{
    /// <summary>
    /// 集合扩展
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }
    }
}
