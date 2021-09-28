using DotNetCore.CAP;
using MediatR;
using System;

namespace NMS.User.Service.IntegrationEvents
{
    public class SubscriberService: ISubscriberService, ICapSubscribe
    {
        private readonly IMediator _mediator;

        public SubscriberService(IMediator mediator)
        {
            this._mediator = mediator;
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="datetime"></param>
        [CapSubscribe("user.services.show.time")]
        public void CheckReceivedMessage(DateTime datetime)
        {
            var result = datetime;
        }
    }
}
