using Hospital.IntegrationTests.Setup;
using Hospital.Presentation;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using Hospital.Presentation.Dto.Record;
using Hospital.Application.MedicalRecords.Responses;
using System.Text.Json;

namespace Hospital.IntegrationTests.RegularMedicalRecordController.Test
{
    public class RegularMedicalRecordPostRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public RegularMedicalRecordPostRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddRegularMedicalRecord_ReturnsCreatedResult_WithAddedRegularMedicalRecordId()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new RegularMedicalRecordRequestDto
            {
                PatientId = 1,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case"
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/RegularMedicalRecord", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var recordId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(4, recordId);
        }

        [Fact]
        public async Task AddRegularMedicalRecord_ReturnsNotFoundResult_PatientDoesNotExist()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new RegularMedicalRecordRequestDto
            {
                PatientId = 5,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case"
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/RegularMedicalRecord", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddRegularMedicalRecord_ReturnsNotFoundResult_DoctorDoesNotExist()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new RegularMedicalRecordRequestDto
            {
                PatientId = 1,
                DoctorId = 5,
                ExaminationNotes = "Notes on the case"
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/RegularMedicalRecord", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddRegularMedicalRecord_ReturnsBadRequestResult_PatientIsNotAssignedToTheDoctor()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new RegularMedicalRecordRequestDto
            {
                PatientId = 3,
                DoctorId = 2,
                ExaminationNotes = "Notes on the case"
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/RegularMedicalRecord", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddRegularMedicalRecord_ReturnsBadRequestResult_InvalidRequestModel()
        {
            var client = _factory.CreateClient();

            var newRecordDto = new RegularMedicalRecordRequestDto
            {
                PatientId = 1,
                DoctorId = 2,
                ExaminationNotes = "N"
            };
            var content = new StringContent(JsonSerializer.Serialize(newRecordDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/RegularMedicalRecord", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SearchRegularMedicalRecordsByASetOfProperties_ReturnsOkResult_WithListOfRegularMedicalRecordFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var filter = new RegularMedicalRecordFilterRequestDto
            {
                ExaminedPatientId = 1,
                ResponsibleDoctorId = 1
            };

            var response = await client.PostAsJsonAsync("/api/RegularMedicalRecord/Search", filter);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var records = await response.Content.ReadFromJsonAsync<List<RegularMedicalRecordFullInfoDto>>();
            Assert.NotNull(records);
            Assert.Single(records);
        }

        [Fact]
        public async Task SearchRegularMedicalRecordsByASetOfProperties_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var filter = new RegularMedicalRecordFilterRequestDto
            {
                ExaminedPatientId = 2
            };

            var response = await client.PostAsJsonAsync("/api/RegularMedicalRecord/Search", filter);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
