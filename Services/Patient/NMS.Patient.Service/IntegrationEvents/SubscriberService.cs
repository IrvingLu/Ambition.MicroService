
using DotNetCore.CAP;
using System;

namespace NMS.Patient.Service.IntegrationEvents
{
    public class SubscriberService: ISubscriberService, ICapSubscribe
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="datetime"></param>
        [CapSubscribe("patient.services.show.time")]
        public void CheckReceivedMessage(DateTime datetime)
        {
            var result = datetime;
        }
    }
}
