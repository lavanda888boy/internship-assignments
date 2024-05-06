namespace Hospital.Domain.Models
{
    public class DoctorsPatients
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; private set; }

        public int PatientId { get; set;}
        public Patient Patient { get; private set; }
    }
}
