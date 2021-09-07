using MediatR;
using NMS.Patient.Service.Patient.Dto;
using Shared.Infrastructure.Core.BaseDto;
using System;

namespace NMS.Patient.Service.Patient.Command
{
    public class PatientDetailCommand : EntityDto, IRequest<PatientDetailDto>
    {
        public PatientDetailCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
