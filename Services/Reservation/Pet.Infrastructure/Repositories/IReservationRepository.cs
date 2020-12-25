using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core;

namespace Pet.Reservation.Infrastructure.Repositories
{
    /// <summary>
    /// 功能描述    ：IReservationRepository  
    /// 创 建 者    ：Administrator
    /// 创建日期    ：2020/12/25 13:17:23 
    /// 最后修改者  ：Administrator
    /// 最后修改日期：2020/12/25 13:17:23 
    /// </summary>
    public interface IReservationRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

    }
}
