using DotNetCore.CAP;
using System;

namespace NMS.User.Service.IntegrationEvents
{
    public interface ISubscriberService
    {
        [CapSubscribe("xxx.services.show.time", false)]
        void CheckReceivedMessage(DateTime datetime);
    }
}
