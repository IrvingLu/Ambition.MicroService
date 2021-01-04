using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core;

namespace Pet.Infrastructure.Repositories
{
    /// <summary>
    /// 功能描述    ：UserRepository  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:13:57 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:13:57 
    /// </summary>
    public class PetRepository<TEntity> : Repository<TEntity, ApplicationDbContext>, IPetRepository<TEntity> where TEntity : Entity
    {
        public PetRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
