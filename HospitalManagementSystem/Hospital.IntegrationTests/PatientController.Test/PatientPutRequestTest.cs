using Hospital.IntegrationTests.Setup;
using Hospital.Presentation;
using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation.Dto.Patient;
using System.Text;
using System.Text.Json;

namespace Hospital.IntegrationTests.PatientController.Test
{
    public class PatientPutRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public PatientPutRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ReturnsOkResult_WithUpdatedPatientId()
        {
            var client = _factory.CreateClient();

            var updatedPatientDto = new PatientRequestDto
            {
                Name = "Mike",
                Surname = "Douglas",
                Age = 35,
                Gender = "M",
                Address = "Cahul, Moldova",
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedPatientDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Patient/Info/{1}", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var patientId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(1, patientId);
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var updatedPatientDto = new PatientRequestDto
            {
                Name = "Mike",
                Surname = "Douglas",
                Age = 35,
                Gender = "M",
                Address = "Cahul, Moldova",
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedPatientDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Patient/Info/{5}", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ReturnsBadRequestResult_InvalidRequestModel()
        {
            var client = _factory.CreateClient();

            var updatedPatientDto = new PatientRequestDto
            {
                Name = "Mike",
                Surname = "Douglas",
                Age = 35,
                Gender = "Some gender",
                Address = "Cahul, Moldova",
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedPatientDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Patient/Info/{1}", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePatientAssignedDoctors_ReturnsOkResult_WithUpdatedPatientId()
        {
            var client = _factory.CreateClient();

            var doctorIds = new List<int> { 1, 2 };
            var queryString = string.Join("&", doctorIds.Select(id => $"doctorIds={id}"));

            var response = await client.PutAsync($"/api/Patient/Doctors/{1}?{queryString}", null);

            var patientId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(1, patientId);
        }

        [Fact]
        public async Task UpdatePatientAssignedDoctors_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var doctorIds = new List<int> { 1, 2 };
            var queryString = string.Join("&", doctorIds.Select(id => $"doctorIds={id}"));

            var response = await client.PutAsync($"/api/Patient/Doctors/{100}?{queryString}", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
