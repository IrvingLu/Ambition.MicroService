using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Dapper
{
    /// <summary>
    /// 功能描述    ：IDapperQuery  
    /// 创 建 者    ：鲁岩奇
    /// 创建日期    ：2020/12/30 9:48:48 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/30 9:48:48 
    /// </summary>
    public interface IDapperQuery
    {
        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param);
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param);
        Task<TEntity> QueryFirstAsync<TEntity>(string sql, object param);
    }
}
