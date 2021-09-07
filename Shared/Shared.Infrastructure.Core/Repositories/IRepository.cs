using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Repositories
{
    /// <summary>
    /// 功能描述    ：IUnitRepository  
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2021/2/5 11:14:12 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2021/2/5 11:14:12 
    /// </summary>
    public interface IRepository<TEntity> where TEntity : Entity 
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(object id);
    }
}
