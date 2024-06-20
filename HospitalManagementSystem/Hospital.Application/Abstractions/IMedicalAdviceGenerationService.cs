namespace Hospital.Application.Abstractions
{
    public interface IMedicalAdviceGenerationService
    {
        Task<string> GenerateMedicalAdviceForPatient(IEnumerable<string> patientRecentIllnesses);
    }
}
