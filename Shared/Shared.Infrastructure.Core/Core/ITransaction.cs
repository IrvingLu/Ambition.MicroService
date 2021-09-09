/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：事务接口
*使用说明    ：事务接口
***********************************************************************/

using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Core
{
    public interface ITransaction
    {
        IDbContextTransaction GetCurrentTransaction();
        bool HasActiveTransaction { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollbackTransaction();
    }
}
