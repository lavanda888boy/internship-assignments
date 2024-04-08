namespace Hospital.Application.Patients.Responses
{
    public class PatientFilterDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
    }
}
