/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：仓储类
*使用说明    ：用于数据操作
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Repositories
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : Entity where TDbContext : DbContext
    {
        protected virtual TDbContext DbContext { get; set; }
        public virtual IUnitOfWork UnitOfWork => (IUnitOfWork)DbContext;
        /// <summary>
        /// 列表
        /// </summary>
        public virtual IQueryable<TEntity> Table => DbContext.Set<TEntity>();
        /// <summary>
        ///列表 AsNoTracking
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => DbContext.Set<TEntity>().AsNoTracking();
        private DbSet<TEntity> _entities;
        protected virtual DbSet<TEntity> Entities => _entities ??= DbContext.Set<TEntity>();

        public Repository(TDbContext context)
        {
            this.DbContext = context;
        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task AddAsync(TEntity entity)
        {
            return Task.FromResult(Entities.AddAsync(entity));
        }
        /// <summary>
        /// 多条新增
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual Task AddEnumerableAsync(IEnumerable<TEntity> entities)
        {
            return Task.FromResult(Entities.AddRangeAsync(entities));
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Entities.Update(entity));
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task RemoveAsync(TEntity entity)
        {
            return Task.FromResult(Entities.Remove(entity));
        }
    }
}
