using AutoMapper;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;

namespace Hospital.Application.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorFullInfoDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Patients, opt => opt.MapFrom(src => src.DoctorsPatients.Select(dp => dp.Patient)))
                .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => new DoctorScheduleDto()
                {
                    StartShift = src.WorkingHours.StartShift,
                    EndShift = src.WorkingHours.EndShift,
                    WeekDays = src.WorkingHours.DoctorScheduleWeekDay.Select(dsw => dsw.WeekDay.DayOfWeek).ToList()
                }));

            CreateMap<Patient, PatientShortInfoDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));

            CreateMap<DoctorSchedule, DoctorScheduleDto>();
        }
    }
}
