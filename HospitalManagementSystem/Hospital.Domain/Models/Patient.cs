using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Patient
    {
        [Column("PatientId")]
        public required int Id { get; set; }

        [MinLength(1)]
        [MaxLength(20)]
        public required string Name { get; set; }

        [MinLength(1)]
        [MaxLength(20)]
        public required string Surname { get; set; }

        [Range(1, 110)]
        public required int Age { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public required string Gender { get; set; }

        [MinLength(10)]
        [MaxLength(30)]
        public required string Address { get; set; }

        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
        public ICollection<Doctor> AssignedDoctor { get; set; } = [];

        public void AddDoctor(Doctor doctor)
        {
            AssignedDoctor.Add(doctor);
        }

        public void RemoveDoctor(int doctorId)
        {
            var doctorToRemove = AssignedDoctor.First(d => d.Id == doctorId);
            AssignedDoctor.Remove(doctorToRemove);
        }
    }

}
