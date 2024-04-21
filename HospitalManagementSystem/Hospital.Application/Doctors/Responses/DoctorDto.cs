using Hospital.Domain.Models;

namespace Hospital.Application.Doctors.Responses
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required Department Department { get; set; }
        public ICollection<Patient> AssignedPatients { get; set; } = new List<Patient>();
        public required DoctorWorkingHours WorkingHours { get; set; }

        public static DoctorDto FromDoctor(Doctor doctor)
        {
            return new DoctorDto()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Address = doctor.Address,
                PhoneNumber = doctor.PhoneNumber,
                Department = doctor.Department,
                AssignedPatients = doctor.AssignedPatient,
                WorkingHours = doctor.WorkingHours
            };
        }
    }
}
