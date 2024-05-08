using Hospital.Application.Doctors.Responses;

namespace Hospital.Application.Patients.Responses
{
    public class PatientFullInfoDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Surname { get; init; }
        public required int Age { get; init; }
        public required string Gender { get; init; }
        public required string Address { get; init; }
        public string? PhoneNumber { get; init; }
        public string? InsuranceNumber { get; init; }
        public ICollection<DoctorShortInfoDto> Doctors { get; init; }
    }
}
