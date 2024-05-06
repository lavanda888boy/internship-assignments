﻿namespace Hospital.Application.Doctors.Responses
{
    public class DoctorScheduleDto
    {
        public required TimeSpan StartShift { get; init; }
        public required TimeSpan EndShift { get; init; }
        public required ICollection<string> WeekDays { get; init; }
    }
}
