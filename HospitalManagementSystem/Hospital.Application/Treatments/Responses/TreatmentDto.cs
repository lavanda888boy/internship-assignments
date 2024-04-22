using Hospital.Domain.Models;

namespace Hospital.Application.Treatments.Responses
{
    public class TreatmentDto
    {
        public required int Id { get; set; }
        public required string PrescribedMedicine { get; set; }
        public TimeSpan TreatmentDuration { get; set; }

        public static TreatmentDto FromTreatment(Treatment treatment)
        {
            return new TreatmentDto()
            {
                Id = treatment.Id,
                PrescribedMedicine = treatment.PrescribedMedicine,
                TreatmentDuration = treatment.Duration,
            };
        }
    }
}
