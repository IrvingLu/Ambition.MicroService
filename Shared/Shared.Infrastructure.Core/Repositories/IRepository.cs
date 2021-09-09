/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：仓储接口类
*使用说明    ：仓储类继承
***********************************************************************/

using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity 
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task<TEntity> FindByIdAsync(object id);
    }
}
