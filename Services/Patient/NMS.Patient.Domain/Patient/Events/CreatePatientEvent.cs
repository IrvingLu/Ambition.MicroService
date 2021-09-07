using Shared.Domain.Abstractions;

namespace NMS.Patient.Domain.Patient.Events
{

    public class CreatePatientEvent : IDomainEvent
    {
        public Patient Patient { get; private set; }
        public CreatePatientEvent(Patient patient)
        {
            this.Patient = patient;
        }
    }
}
