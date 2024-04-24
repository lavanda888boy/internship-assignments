using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    [Table("Weekdays")]
    public class WeekDay
    {
        [Column("WeekDayId")]
        public int Id { get; set; }

        [MaxLength(10)]
        public required string DayOfWeek { get; set; }

        public ICollection<DoctorScheduleWeekDay> DoctorScheduleWeekDay { get; set; } = [];
    }
}
