using Shared.Domain.Abstractions;

namespace NMS.User.Domain.Tenant
{
    /// <summary>
    /// 功能描述    ：租户 
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/25 13:48:53 
    /// 最后修改者  ：鲁岩奇
    /// 最后修改日期：2020/12/25 13:48:53 
    /// </summary>
    public class Tenant : Entity, IAggregateRoot
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; } 
    }
}
