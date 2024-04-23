using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;

namespace Hospital.Application.Illnesses.Responses
{
    public class IllnessDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public IllnessSeverity IllnessSeverity { get; set; }

        public static IllnessDto FromIllness(Illness illness)
        {
            return new IllnessDto()
            {
                Id = illness.Id,
                Name = illness.Name,
                IllnessSeverity = illness.Severity,
            };
        }
    }
}
