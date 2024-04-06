using Hospital.Domain.Models;

namespace Hospital.Application.WorkingHours.Responses
{
    public class DoctorWorkingHoursDto
    {
        public required int Id { get; set; }
        public required TimeSpan StartShift { get; set; }
        public required TimeSpan EndShift { get; set; }

        public static DoctorWorkingHoursDto FromDoctorWorkingHours(DoctorWorkingHours workingHours)
        {
            return new DoctorWorkingHoursDto()
            {
                Id = workingHours.Id,
                StartShift = workingHours.StartShift,
                EndShift = workingHours.EndShift,
            };
        }
    }
}
