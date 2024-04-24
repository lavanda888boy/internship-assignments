namespace Hospital.Domain.Models
{
    public class DoctorsPatients
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public int PatientId { get; set;}
        public Patient Patient { get; set; } = null!;
    }
}
