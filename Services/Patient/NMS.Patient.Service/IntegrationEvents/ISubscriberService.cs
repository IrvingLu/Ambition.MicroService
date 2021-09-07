using DotNetCore.CAP;
using System;

namespace NMS.Patient.Service.IntegrationEvents
{
    public interface ISubscriberService
    {
        [CapSubscribe("patient.services.show.time", false)]
        void CheckReceivedMessage(DateTime datetime);
    }
}
