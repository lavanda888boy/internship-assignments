namespace Hospital.Domain.Models
{
    public class Patient
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required int Age { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
        public ICollection<Doctor> AssignedDoctors { get; set; } = new List<Doctor>();

        public void AddDoctor(Doctor doctor)
        {
            AssignedDoctors.Add(doctor);
        }

        public void RemoveDoctor(int doctorId)
        {
            var doctorToRemove = AssignedDoctors.First(d => d.Id == doctorId);
            AssignedDoctors.Remove(doctorToRemove);
        }
    }

}
