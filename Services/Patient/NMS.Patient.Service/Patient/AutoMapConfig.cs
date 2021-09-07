using AutoMapper;
using NMS.Patient.Service.Patient.Command;
using NMS.Patient.Service.Patient.Dto;

namespace NMS.Patient.Service.Patient
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<Domain.Patient.Patient, PatientDetailDto>();
            CreateMap<Domain.Patient.Patient, PatientListViewDto>();


            CreateMap<UpdatePatientCommand, Domain.Patient.Patient>();
        }
    }
}
