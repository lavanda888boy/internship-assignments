using Hospital.IntegrationTests.Setup;
using Hospital.Presentation.Dto.Patient;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using Hospital.Presentation;
using Hospital.Presentation.Dto.Doctor;
using System.Text.Json;

namespace Hospital.IntegrationTests.DoctorController.Test
{
    public class DoctorPutRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DoctorPutRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UpdateDoctorPersonalInfo_ReturnsOkResult_WithUpdatedPatientId()
        {
            var client = _factory.CreateClient();

            var updatedDoctorDto = new DoctorRequestDto
            {
                Name = "Mike",
                Surname = "Douglas",
                PhoneNumber = "022767828",
                Address = "Cahul, Moldova",
                DepartmentId = 1,
                StartShift = "02:00:00",
                EndShift = "10:00:00",
                WeekDayIds = new List<int> { 1, 2, 3 }  
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedDoctorDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Doctor/Info/{1}", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doctorId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(1, doctorId);
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ReturnsNotFoundResult_DepartmentDoesNotExist()
        {
            var client = _factory.CreateClient();

            var updatedDoctorDto = new DoctorRequestDto
            {
                Name = "Mike",
                Surname = "Douglas",
                PhoneNumber = "022767828",
                Address = "Cahul, Moldova",
                DepartmentId = 5,
                StartShift = "02:00:00",
                EndShift = "10:00:00",
                WeekDayIds = new List<int> { 1, 2, 3 }
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedDoctorDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Doctor/Info/{1}", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ReturnsNotFoundResult_NoDoctorToUpdate()
        {
            var client = _factory.CreateClient();

            var updatedDoctorDto = new DoctorRequestDto
            {
                Name = "Mike",
                Surname = "Douglas",
                PhoneNumber = "022767828",
                Address = "Cahul, Moldova",
                DepartmentId = 1,
                StartShift = "02:00:00",
                EndShift = "10:00:00",
                WeekDayIds = new List<int> { 1, 2, 3 }
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedDoctorDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Doctor/Info/{5}", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ReturnsBadRequestResult_InvalidRequestModel()
        {
            var client = _factory.CreateClient();

            var updatedDoctorDto = new DoctorRequestDto
            {
                Name = "Mike",
                Surname = "Douglas",
                PhoneNumber = "022767828",
                Address = "Cahul, Moldova",
                DepartmentId = 1,
                StartShift = "02:00:00:00:00",
                EndShift = "10:00:00",
                WeekDayIds = new List<int> { 1, 2, 3 }
            };
            var content = new StringContent(JsonSerializer.Serialize(updatedDoctorDto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Patient/Info/{1}", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateDoctorAssignedPatients_ReturnsOkResult_WithUpdatedDoctorId()
        {
            var client = _factory.CreateClient();

            var doctorIds = new List<int> { 3 };
            var queryString = string.Join("&", doctorIds.Select(id => $"patientIds={id}"));

            var response = await client.PutAsync($"/api/Doctor/Patients/{1}?{queryString}", null);

            var doctorId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(1, doctorId);
        }

        [Fact]
        public async Task UpdateDoctorAssignedPatients_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var doctorIds = new List<int> { 3 };
            var queryString = string.Join("&", doctorIds.Select(id => $"patientIds={id}"));

            var response = await client.PutAsync($"/api/Doctor/Patients/{100}?{queryString}", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
