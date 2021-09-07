using DotNetCore.CAP;
using System;

namespace NMS.User.Service.IntegrationEvents
{
    public class SubscriberService: ISubscriberService, ICapSubscribe
    {
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
