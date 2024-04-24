using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    [Table("Weekdays")]
    public class WeekDay
    {
        [Column("WeekDayId")]
        public int Id { get; set; }

        public required DayOfWeek DayOfWeek { get; set; }
        public ICollection<DoctorScheduleWeekDay> DoctorScheduleWeekDay { get; set; } = [];
    }
}
