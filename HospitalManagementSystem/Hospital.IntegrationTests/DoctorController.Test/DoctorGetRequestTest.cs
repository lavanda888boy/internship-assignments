using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;
using Hospital.Application.Doctors.Responses;
using Hospital.IntegrationTests.Setup;

namespace Hospital.IntegrationTests.DoctorController.Test
{
    public class DoctorGetRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DoctorGetRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllDoctors_ReturnsOkResult_WithListOfDoctorFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Doctor");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doctors = await response.Content.ReadFromJsonAsync<List<DoctorFullInfoDto>>();
            Assert.NotNull(doctors);
            Assert.NotEmpty(doctors);
        }

        [Fact]
        public async Task GetDoctorById_ReturnsOkResult_WithDoctorFullInfoDto()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Doctor/1");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doctor = await response.Content.ReadFromJsonAsync<DoctorFullInfoDto>();
            Assert.NotNull(doctor);
            Assert.Equal(1, doctor.Id);
        }

        [Fact]
        public async Task GetDoctorById_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Doctor/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
