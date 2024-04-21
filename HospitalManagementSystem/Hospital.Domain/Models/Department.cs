using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Department
    {
        [Column("DepartmentId")]
        public required int Id { get; set; }

        [MinLength(5)]
        [MaxLength(20)]
        public required string Name { get; set; }
    }
}
