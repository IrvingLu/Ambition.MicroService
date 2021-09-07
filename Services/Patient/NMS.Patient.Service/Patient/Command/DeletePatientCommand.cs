using MediatR;
using Shared.Infrastructure.Core.BaseDto;
using System;

namespace NMS.Patient.Service.Patient.Command
{
    public class DeletePatientCommand:EntityDto,IRequest
    {
        public DeletePatientCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
