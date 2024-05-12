using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;
using Hospital.IntegrationTests.Setup;

namespace Hospital.IntegrationTests.PatientController.Test
{
    public class PatientDeleteRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public PatientDeleteRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeletePatient_ReturnsOkResult_WithDeletedPatientId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/Patient/2");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var patientId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(2, patientId);
        }

        [Fact]
        public async Task DeletePatient_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/Patient/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
