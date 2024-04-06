namespace Hospital.Domain.Models
{
    public class Doctor
    {
        private int _assignedPatientsLimit = 5;
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required Department Department { get; set; }
        public ICollection<Patient> AssignedPatients { get; set; } = new List<Patient>();
        public required DoctorWorkingHours WorkingHours { get; set; }
    }
}
