using Hospital.IntegrationTests.Setup;
using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;

namespace Hospital.IntegrationTests.DoctorController.Test
{
    public class DoctorDeleteRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DoctorDeleteRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DeleteDoctor_ReturnsOkResult_WithDeletedDoctorId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/Doctor/2");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doctorId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(2, doctorId);
        }

        [Fact]
        public async Task DeleteDoctor_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("/api/Doctor/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
