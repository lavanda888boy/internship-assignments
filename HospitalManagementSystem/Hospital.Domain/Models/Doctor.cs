namespace Hospital.Domain.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required Department Department { get; set; }
    }
}
