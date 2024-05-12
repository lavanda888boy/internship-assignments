using Hospital.IntegrationTests.Setup;
using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;

namespace Hospital.IntegrationTests.DiagnosisMedicalRecordController.Test
{
    public class DiagnosisMedicalRecordDeleteRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DiagnosisMedicalRecordDeleteRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeleteDiagnosisMedicalRecord_ReturnsOkResult_WithDeletedDiagnosisMedicalRecordId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/DiagnosisMedicalRecord/2");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var recordId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(2, recordId);
        }

        [Fact]
        public async Task DeleteDiagnosisMedicalRecord_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/DiagnosisMedicalRecord/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
