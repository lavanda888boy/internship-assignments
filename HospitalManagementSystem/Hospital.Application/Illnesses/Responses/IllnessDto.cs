using Hospital.Domain.Models;

namespace Hospital.Application.Illnesses.Responses
{
    public class IllnessDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateTimeOffset DateOfDiagnosis { get; set; }
        public DateTimeOffset? DateOfTreatmentEnd { get; set; }
        public required Doctor DiagnosisDoctor { get; set; }

        public static IllnessDto FromIllness(Illness illness)
        {
            return new IllnessDto()
            {
                Id = illness.Id,
                Name = illness.Name,
                DateOfDiagnosis = illness.DateOfDiagnosis,
                DateOfTreatmentEnd = illness.DateOfTreatmentEnd,
                DiagnosisDoctor = illness.DiagnosisDoctor
            };
        }
    }
}
