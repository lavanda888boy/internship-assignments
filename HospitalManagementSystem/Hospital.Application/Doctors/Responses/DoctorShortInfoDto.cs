namespace Hospital.Application.Doctors.Responses
{
    public class DoctorShortInfoDto
    {
        public required string Name { get; init; }
        public required string Surname { get; init; }
        public required string Department { get; init; }
    }
}
