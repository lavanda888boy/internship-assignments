using Hospital.Application.MedicalRecords.Responses;
using Hospital.IntegrationTests.Setup;
using Hospital.Presentation.Dto.Record;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using Hospital.Presentation;
using System.Text.Json;

namespace Hospital.IntegrationTests.DiagnosisMedicalRecordController.Test
{
    public class DiagnosisMedicalRecordPostRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DiagnosisMedicalRecordPostRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddDiagnosisMedicalRecord_ReturnsCreatedResult_WithAddedDiagnosisMedicalRecordId()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new DiagnosisMedicalRecordRequestDto
            {
                PatientId = 1,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case",
                IllnessId = 1,
                PrescribedMedicine = "Aspirin",
                Duration = 5
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/DiagnosisMedicalRecord", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var recordId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(4, recordId);
        }

        [Fact]
        public async Task AddDiagnosisMedicalRecord_ReturnsNotFoundResult_PatientDoesNotExist()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new DiagnosisMedicalRecordRequestDto
            {
                PatientId = 5,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case",
                IllnessId = 1,
                PrescribedMedicine = "Aspirin",
                Duration = 5
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/DiagnosisMedicalRecord", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddDiagnosisMedicalRecord_ReturnsNotFoundResult_DoctorDoesNotExist()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new DiagnosisMedicalRecordRequestDto
            {
                PatientId = 1,
                DoctorId = 5,
                ExaminationNotes = "Notes on the case",
                IllnessId = 1,
                PrescribedMedicine = "Aspirin",
                Duration = 5
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/DiagnosisMedicalRecord", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddDiagnosisMedicalRecord_ReturnsNotFoundResult_IllnessDoesNotExist()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new DiagnosisMedicalRecordRequestDto
            {
                PatientId = 1,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case",
                IllnessId = 2,
                PrescribedMedicine = "Aspirin",
                Duration = 5
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/DiagnosisMedicalRecord", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddDiagnosisMedicalRecord_ReturnsBadRequestResult_PatientIsNotAssignedToTheDoctor()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new DiagnosisMedicalRecordRequestDto
            {
                PatientId = 3,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case",
                IllnessId = 1,
                PrescribedMedicine = "Aspirin",
                Duration = 5
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/DiagnosisMedicalRecord", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddDiagnosisMedicalRecord_ReturnsBadRequestResult_InvalidRequestModel()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new DiagnosisMedicalRecordRequestDto
            {
                PatientId = 1,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case",
                IllnessId = 1,
                PrescribedMedicine = "Aspirin",
                Duration = 100
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/DiagnosisMedicalRecord", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SearchDiagnosisMedicalRecordsByASetOfProperties_ReturnsOkResult_WithListOfDiagnosisMedicalRecordFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var filter = new DiagnosisMedicalRecordFilterRequestDto
            {
                PrescribedMedicine = "Valocordin"
            };

            var response = await client.PostAsJsonAsync("/api/DiagnosisMedicalRecord/Search", filter);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var records = await response.Content.ReadFromJsonAsync<List<DiagnosisMedicalRecordFilterRequestDto>>();
            Assert.NotNull(records);
            Assert.Single(records);
        }

        [Fact]
        public async Task SearchDiagnosisMedicalRecordsByASetOfProperties_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var filter = new DiagnosisMedicalRecordFilterRequestDto
            {
                DiagnosedIllnessName = "Flu",
                PrescribedMedicine = "Valocordin"
            };

            var response = await client.PostAsJsonAsync("/api/DiagnosisMedicalRecord/Search", filter);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
