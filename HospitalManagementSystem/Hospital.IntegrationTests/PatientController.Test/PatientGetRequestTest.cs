using Hospital.Application.Patients.Responses;
using Hospital.IntegrationTests.Setup;
using Hospital.Presentation;
using System.Net;
using System.Net.Http.Json;

namespace Hospital.IntegrationTests.PatientController.Test
{
    public class PatientGetRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public PatientGetRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllPatients_ReturnsOkResult_WithListOfPatientFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Patient");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var patients = await response.Content.ReadFromJsonAsync<List<PatientFullInfoDto>>();
            Assert.NotNull(patients);
            Assert.Equal(3, patients.Count);
        }

        [Fact]
        public async Task GetPatientById_ReturnsOkResult_WithPatientFullInfoDto()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Patient/1");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var patient = await response.Content.ReadFromJsonAsync<PatientFullInfoDto>();
            Assert.NotNull(patient);
            Assert.Equal(1, patient.Id);
        }

        [Fact]
        public async Task GetPatientById_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Patient/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
