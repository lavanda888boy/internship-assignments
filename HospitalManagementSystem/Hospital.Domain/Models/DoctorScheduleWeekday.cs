namespace Hospital.Domain.Models
{
    public class DoctorScheduleWeekDay
    {
        public int DoctorScheduleId { get; set; }
        public DoctorSchedule DoctorSchedule { get; set; } = null!;

        public int WeekDayId { get; set; }
        public WeekDay WeekDay { get; set; } = null!;
    }
}
