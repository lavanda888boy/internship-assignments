namespace Hospital.Domain.Models
{
    public class Department
    {
        private int _maximumMedicalStaffNumber = 10;
        public required int Id { get; set; }
        public required string Name { get; set; }
        public int MedicalStaffCount {  get; set; } = 0;
    }
}
