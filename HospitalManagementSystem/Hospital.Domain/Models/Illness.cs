using Hospital.Domain.Models.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Domain.Models
{
    public class Illness
    {
        [Column("IllnessId")]
        public required int Id { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        public required string Name { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public IllnessSeverity IllnessSeverity { get; set; }
    }
}
