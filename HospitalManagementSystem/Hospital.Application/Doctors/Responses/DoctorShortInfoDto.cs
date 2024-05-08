namespace Hospital.Application.Doctors.Responses
{
    public class DoctorShortInfoDto
    {
        public int Id { get; init; }
        public required string FullName { get; init; }
        public required string Department { get; init; }
    }
}
