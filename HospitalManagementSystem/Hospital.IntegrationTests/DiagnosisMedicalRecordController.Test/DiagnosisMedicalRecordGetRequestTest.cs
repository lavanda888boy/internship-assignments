using Hospital.Application.MedicalRecords.Responses;
using System.Net.Http.Json;
using System.Net;
using Hospital.Presentation;
using Hospital.IntegrationTests.Setup;

namespace Hospital.IntegrationTests.DiagnosisMedicalRecordController.Test
{
    public class DiagnosisMedicalRecordGetRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DiagnosisMedicalRecordGetRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllDiagnosisMedicalRecords_ReturnsOkResult_WithListOfDiagnosisMedicalRecordFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/DiagnosisMedicalRecord");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var records = await response.Content.ReadFromJsonAsync<List<DiagnosisMedicalRecordFullInfoDto>>();
            Assert.NotNull(records);
            Assert.NotEmpty(records);
        }

        [Fact]
        public async Task GetDiagnosisMedicalRecordById_ReturnsOkResult_WithDiagnosisMedicalRecordFullInfoDto()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/DiagnosisMedicalRecord/1");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var record = await response.Content.ReadFromJsonAsync<DiagnosisMedicalRecordFullInfoDto>();
            Assert.NotNull(record);
            Assert.Equal(1, record.Id);
        }

        [Fact]
        public async Task GetDiagnosisMedicalRecordById_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/DiagnosisMedicalRecord/100");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
