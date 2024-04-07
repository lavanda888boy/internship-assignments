namespace Hospital.Domain.Models
{
    public class Department
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public int MedicalStaffCount {  get; set; } = 0;
    }
}
