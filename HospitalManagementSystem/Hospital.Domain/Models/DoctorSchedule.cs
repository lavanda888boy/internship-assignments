using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class DoctorSchedule
    {
        [Column("DoctorScheduleId")]
        public int Id { get; set; }

        public required TimeSpan StartShift { get; set; }
        public required TimeSpan EndShift { get; set; }

        public ICollection<DoctorScheduleWeekDay> DoctorScheduleWeekDay { get; set; } = [];
    }
}
