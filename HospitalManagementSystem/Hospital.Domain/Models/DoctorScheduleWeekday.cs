using System.Text.Json.Serialization;

namespace Hospital.Domain.Models
{
    public class DoctorScheduleWeekDay
    {
        public int DoctorScheduleId { get; set; }

        [JsonIgnore]
        public DoctorSchedule DoctorSchedule { get; private set; }

        public int WeekDayId { get; set; }

        [JsonIgnore]
        public WeekDay WeekDay { get; private set; }
    }
}
