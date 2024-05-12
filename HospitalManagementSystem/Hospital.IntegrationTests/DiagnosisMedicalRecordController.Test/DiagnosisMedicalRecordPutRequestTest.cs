using Hospital.IntegrationTests.Setup;
using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;
using Hospital.Presentation.Dto.Record;

namespace Hospital.IntegrationTests.DiagnosisMedicalRecordController.Test
{
    public class DiagnosisMedicalRecordPutRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DiagnosisMedicalRecordPutRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateDiagnosisMedicalRecordExaminationNotes_ReturnsOkResult_WithUpdatedRecordId()
        {
            var client = _factory.CreateClient();
            var notes = "New examination notes";

            var response = await client.PutAsync($"/api/DiagnosisMedicalRecord/ExaminationNotes/{1}?notes={notes}", null);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var recordId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(1, recordId);
        }

        [Fact]
        public async Task UpdateDiagnosisMedicalRecordExaminationNotes_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();
            var notes = "New examination notes";

            var response = await client.PutAsync($"/api/DiagnosisMedicalRecord/ExaminationNotes/{100}?notes={notes}", null);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateDiagnosisMedicalRecordTreatmentDetails_ReturnsOkResult_WithUpdatedRecordId()
        {
            var client = _factory.CreateClient();
            var treatment = new TreatmentDto
            {
                IllnessId = 1,
                PrescribedMedicine = "Corvalol",
                Duration = 10
            };

            var response = await client.PutAsJsonAsync($"/api/DiagnosisMedicalRecord/Treatment/{1}", treatment);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var recordId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(1, recordId);
        }

        [Fact]
        public async Task UpdateDiagnosisMedicalRecordTreatmentDetails_ReturnsNotFoundResult_RecordToUpdateNotFound()
        {
            var client = _factory.CreateClient();
            var treatment = new TreatmentDto
            {
                IllnessId = 1,
                PrescribedMedicine = "Corvalol",
                Duration = 10
            };

            var response = await client.PutAsJsonAsync($"/api/DiagnosisMedicalRecord/Treatment/{100}", treatment);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateDiagnosisMedicalRecordTreatmentDetails_ReturnsNotFoundResult_IllnessDoesNotExist()
        {
            var client = _factory.CreateClient();
            var treatment = new TreatmentDto
            {
                IllnessId = 2,
                PrescribedMedicine = "Corvalol",
                Duration = 10
            };

            var response = await client.PutAsJsonAsync($"/api/DiagnosisMedicalRecord/Treatment/{1}", treatment);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
