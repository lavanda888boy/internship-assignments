using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.IntegrationTests.Setup;

namespace Hospital.IntegrationTests.RegularMedicalRecordController.Test
{
    public class RegularMedicalRecordGetRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public RegularMedicalRecordGetRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllRegularMedicalRecords_ReturnsOkResult_WithListOfRegularMedicalRecordFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/RegularMedicalRecord");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var records = await response.Content.ReadFromJsonAsync<List<RegularMedicalRecordFullInfoDto>>();
            Assert.NotNull(records);
            Assert.NotEmpty(records);
        }

        [Fact]
        public async Task GetRegularMedicalRecordById_ReturnsOkResult_WithRegularMedicalRecordFullInfoDto()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/RegularMedicalRecord/1");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var record = await response.Content.ReadFromJsonAsync<RegularMedicalRecordFullInfoDto>();
            Assert.NotNull(record);
            Assert.Equal(1, record.Id);
        }

        [Fact]
        public async Task GetRegularMedicalRecordById_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/RegularMedicalRecord/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
