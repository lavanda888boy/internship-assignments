using AutoMapper;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Illnesses.Responses;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Application.Treatments.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;

namespace Hospital.Application.Profiles
{
    public class DiagnosisMedicalRecordProfile : Profile
    {
        public DiagnosisMedicalRecordProfile()
        {
            CreateMap<DiagnosisMedicalRecord, DiagnosisMedicalRecordDto>();
            CreateMap<Patient, PatientRecordDto>();

            CreateMap<Doctor, DoctorRecordDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<Illness, IllnessRecordDto>()
                .ForMember(dest => dest.IllnessSeverity, opt => opt.MapFrom(src => Enum.GetName(typeof(IllnessSeverity), src.Severity)));
            CreateMap<Treatment, TreatmentRecordDto>();
        }
    }
}
