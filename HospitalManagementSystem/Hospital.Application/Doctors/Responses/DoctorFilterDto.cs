namespace Hospital.Application.Doctors.Responses
{
    public class DoctorFilterDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
    }
}
