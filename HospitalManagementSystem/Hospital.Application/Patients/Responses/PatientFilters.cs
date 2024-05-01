using Hospital.Domain.Models.Utility;

namespace Hospital.Application.Patients.Responses
{
    public class PatientFilters
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
    }
}
