/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：Dapper接口
*使用说明    ：Dapper接口
***********************************************************************/

using System.Collections.Generic;
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
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param);
        Task<TEntity> QueryFirstAsync<TEntity>(string sql, object param);
    }
}
