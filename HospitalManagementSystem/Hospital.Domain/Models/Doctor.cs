using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Doctor
    {
        private int _assignedPatientsLimit = 2;

        [Column("DoctorId")]
        public required int Id { get; set; }

        [MinLength(1)]
        [MaxLength(20)]
        public required string Name { get; set; }

        [MinLength(1)]
        [MaxLength(20)]
        public required string Surname { get; set; }

        [MinLength(10)]
        [MaxLength(30)]
        public required string Address { get; set; }

        [MinLength(8)]
        [MaxLength(10)]
        public required string PhoneNumber { get; set; }

        public required Department Department { get; set; }
        public ICollection<Patient> AssignedPatient { get; set; } = [];

        public required int WorkingHoursId { get; set; }
        public required DoctorWorkingHours WorkingHours { get; set; }

        public bool TryAddPatient(Patient patient)
        {
            if (AssignedPatient.Count + 1 > _assignedPatientsLimit ||
                AssignedPatient.Any(p => p.Id == patient.Id))
            {
                return false;
            }

            AssignedPatient.Add(patient);
            return true;
        }

        public void RemovePatient(int patientId)
        {
            var patientToRemove = AssignedPatient.First(p => p.Id == patientId);
            AssignedPatient.Remove(patientToRemove);
        }
    }
}
