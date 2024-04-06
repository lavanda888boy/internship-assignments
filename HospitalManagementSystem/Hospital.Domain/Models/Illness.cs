using Hospital.Domain.Models.Utility;

namespace Hospital.Domain.Models
{
    public class Illness
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public IllnessSeverity IllnessSeverity { get; set; }
    }
}
