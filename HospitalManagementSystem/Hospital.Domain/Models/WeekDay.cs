using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class WeekDay
    {
        [Column("WeekDayId")]
        public required int Id { get; set; }

        public required DayOfWeek DayOfWeek { get; set; }
    }
}
