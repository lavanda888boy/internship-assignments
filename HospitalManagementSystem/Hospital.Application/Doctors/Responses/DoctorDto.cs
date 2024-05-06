using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;

namespace Hospital.Application.Doctors.Responses
{
    public class DoctorDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Surname { get; init; }
        public required string Address { get; init; }
        public required string PhoneNumber { get; init; }
        public required string Department { get; init; }
        public required DoctorScheduleDto WorkingHours { get; init; }
        public required ICollection<PatientShortInfoDto> Patients { get; init; }
    }
}
