namespace Hospital.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; }
        public required ICollection<Doctor> AssignedDoctors { get; set; }
        public ICollection<Illness>? Illnesses {  get; set; }
    }

}
