using Hospital.Domain.Models.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Patient
    {
        [Column("PatientId")]
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public required string Surname { get; set; }

        [Range(1, 150)]
        public required int Age { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public required Gender Gender { get; set; }

        [MinLength(10)]
        [MaxLength(100)]
        public required string Address { get; set; }

        public string? PhoneNumber { get; set; }
        public string? InsuranceNumber { get; set; }
        public ICollection<DoctorsPatients> DoctorsPatients { get; set; } = [];
    }

}
