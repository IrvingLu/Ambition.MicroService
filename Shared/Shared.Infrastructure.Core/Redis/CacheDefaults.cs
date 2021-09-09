/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：redis配置
*使用说明    ：redis配置
***********************************************************************/

namespace Shared.Infrastructure.Core.Redis
{
    public static class CacheDefaults
    {
        /// <summary>
        /// 默认过期时间
        /// </summary>
        public static int CacheTime => 86400;
    }
}
