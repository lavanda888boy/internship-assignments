using Hospital.IntegrationTests.Setup;
using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;

namespace Hospital.IntegrationTests.RegularMedicalRecordController.Test
{
    public class RegularMedicalRecordDeleteRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public RegularMedicalRecordDeleteRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeleteRegularMedicalRecord_ReturnsOkResult_WithDeletedRegularMedicalRecordId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/RegularMedicalRecord/2");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var recordId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(2, recordId);
        }

        [Fact]
        public async Task DeleteRegularMedicalRecord_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/RegularMedicalRecord/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
