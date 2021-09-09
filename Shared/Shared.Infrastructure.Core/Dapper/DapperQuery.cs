/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：Dapper封装
*使用说明    ：Dapper封装
***********************************************************************/

using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Dapper
{
    public class DapperQuery: IDapperQuery
    {
        #region Fields
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        #endregion

        #region Ctor
        public DapperQuery(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("Postgresql");
        }
        #endregion

        #region Methods
        /// <summary>
        /// 查询列表带参数
        /// </summary>
        /// <param name="sql">查询的sql</param>
        /// <param name="param">替换参数</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object param)
        {
            using NpgsqlConnection con = new(connectionString);
            return await con.QueryAsync<TEntity>(sql, param);
        }
        /// <summary>
        /// 查询一条带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryFirstAsync<TEntity>(string sql, object param)
        {
            using NpgsqlConnection con = new(connectionString);
            return await con.QueryFirstOrDefaultAsync<TEntity>(sql, param);
        }
        #endregion
    }
}
