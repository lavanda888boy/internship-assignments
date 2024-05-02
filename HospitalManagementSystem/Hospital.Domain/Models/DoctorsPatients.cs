using System.Text.Json.Serialization;

namespace Hospital.Domain.Models
{
    public class DoctorsPatients
    {
        public int DoctorId { get; set; }

        [JsonIgnore]
        public Doctor Doctor { get; private set; }

        public int PatientId { get; set;}

        [JsonIgnore]
        public Patient Patient { get; private set; }
    }
}
