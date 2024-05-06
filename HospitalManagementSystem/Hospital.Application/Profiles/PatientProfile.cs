using AutoMapper;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;

namespace Hospital.Application.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.GetName(typeof(Gender), src.Gender)))
                .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.DoctorsPatients.Select(dp => dp.Doctor)));

            CreateMap<Doctor, DoctorShortInfoDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));
        }
    }
}
