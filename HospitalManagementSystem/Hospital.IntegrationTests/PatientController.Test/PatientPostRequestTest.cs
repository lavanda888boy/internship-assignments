using Hospital.IntegrationTests.Setup;
using Hospital.Presentation;
using Hospital.Presentation.Dto.Patient;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using Hospital.Application.Patients.Responses;

namespace Hospital.IntegrationTests.PatientController.Test
{
    public class PatientPostRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public PatientPostRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddPatient_ReturnsCreatedResult_WithAddedPatientId()
        {
            var client = _factory.CreateClient();

            var newPatientDto = new PatientRequestDto
            {
                Name = "Buck",
                Surname = "Stevens",
                Age = 65,
                Gender = "M",
                Address = "Cahul, Moldova",
            };
            var content = new StringContent(JsonSerializer.Serialize(newPatientDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Patient", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var patientId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(4, patientId);
        }

        [Fact]
        public async Task AddPatient_ReturnsBadRequestResult_InvalidRequestModel()
        {
            var client = _factory.CreateClient();

            var newPatientDto = new PatientRequestDto
            {
                Name = "Buck",
                Surname = "Stevens",
                Age = 65,
                Gender = "Some gender",
                Address = "Cahul, Moldova",
            };
            var content = new StringContent(JsonSerializer.Serialize(newPatientDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Patient", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SearchPatientsByASetOfProperties_ReturnsOkResult_WithListOfPatientFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var filter = new PatientFilterRequestDto
            {
                Gender = "M",
            };

            var response = await client.PostAsJsonAsync("/api/Patient/Search", filter);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var patients = await response.Content.ReadFromJsonAsync<List<PatientFullInfoDto>>();
            Assert.NotNull(patients);
            Assert.Equal(2, patients.Count);
        }

        [Fact]
        public async Task SearchPatientsByASetOfProperties_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var filter = new PatientFilterRequestDto
            {
                Name = "Mike",
                Age = 70
            };

            var response = await client.PostAsJsonAsync("/api/Patient/Search", filter);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
