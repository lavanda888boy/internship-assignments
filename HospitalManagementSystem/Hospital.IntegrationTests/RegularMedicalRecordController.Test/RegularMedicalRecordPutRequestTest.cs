using Hospital.IntegrationTests.Setup;
using Hospital.Presentation;
using System.Net.Http.Json;
using System.Net;

namespace Hospital.IntegrationTests.RegularMedicalRecordController.Test
{
    public class RegularMedicalRecordPutRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public RegularMedicalRecordPutRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateRegularMedicalRecordExaminationNotes_ReturnsOkResult_WithUpdatedRecordId()
        {
            var client = _factory.CreateClient();
            var notes = "New examination notes";

            var response = await client.PutAsync($"/api/RegularMedicalRecord/{1}?notes={notes}", null);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var recordId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(1, recordId);
        }

        [Fact]
        public async Task UpdateRegularMedicalRecordExaminationNotes_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();
            var notes = "New examination notes";

            var response = await client.PutAsync($"/api/RegularMedicalRecord/{100}?notes={notes}", null);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
