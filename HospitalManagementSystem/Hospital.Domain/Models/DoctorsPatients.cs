namespace Hospital.Domain.Models
{
    public class DoctorsPatients
    {
        public required int DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        public required int PatientId { get; set;}
        public required Patient Patient { get; set;}
    }
}
