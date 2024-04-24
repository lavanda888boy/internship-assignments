using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Doctor
    {
        [Column("DoctorId")]
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public required string Surname { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        public required string Address { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        public required string PhoneNumber { get; set; }

        public required Department Department { get; set; }
        public ICollection<DoctorsPatients> DoctorsPatients { get; set; } = [];

        public required DoctorSchedule WorkingHours { get; set; }
    }
}
