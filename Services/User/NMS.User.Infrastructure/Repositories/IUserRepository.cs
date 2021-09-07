using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.Repositories;

namespace NMS.User.Infrastructure.Repositories
{
    /// <summary>
    /// 功能描述    ：IUserRepository  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public interface IUserRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

    }
}
