namespace Hospital.Domain.Models
{
    public class DoctorWorkingHours
    {
        public required int Id { get; set; }
        public Doctor? Doctor { get; set; }
        public required TimeSpan StartShift { get; set; }
        public required TimeSpan EndShift { get; set; }
    }
}
