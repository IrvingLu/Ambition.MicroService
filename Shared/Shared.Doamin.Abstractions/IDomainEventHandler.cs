/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：领域事件
*使用说明    ：领域事件
***********************************************************************/

using MediatR;

namespace Shared.Domain.Abstractions
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    {

    }
}
