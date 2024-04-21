using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class DoctorWorkingHours
    {
        [Column("DoctorWorkingHoursId")]
        public required int Id { get; set; }

        public required Doctor Doctor { get; set; }
        public required TimeSpan StartShift { get; set; }
        public required TimeSpan EndShift { get; set; }

        public required int WeekDayId { get; set; }
        public required WeekDay WeekDay {  get; set; }
    }
}
