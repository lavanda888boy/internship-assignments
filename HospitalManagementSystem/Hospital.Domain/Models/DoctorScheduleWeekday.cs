using System.Text.Json.Serialization;

namespace Hospital.Domain.Models
{
    public class DoctorScheduleWeekDay
    {
        public int DoctorScheduleId { get; set; }
        public DoctorSchedule DoctorSchedule { get; private set; }

        public int WeekDayId { get; set; }
        public WeekDay WeekDay { get; private set; }
    }
}
