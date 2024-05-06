using AutoMapper;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;

namespace Hospital.Application.Profiles
{
    public class RegularMedicalRecordProfile : Profile
    {
        public RegularMedicalRecordProfile()
        {
            CreateMap<RegularMedicalRecord, RegularMedicalRecordDto>();
            CreateMap<Patient, PatientRecordDto>();

            CreateMap<Doctor, DoctorRecordDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));
        }
    }
}
