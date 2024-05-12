using Hospital.IntegrationTests.Setup;
using Hospital.Presentation;
using System.Net.Http.Json;
using System.Net;
using Hospital.Application.Doctors.Responses;
using Hospital.Presentation.Dto.Doctor;
using System.Text.Json;
using System.Text;

namespace Hospital.IntegrationTests.DoctorController.Test
{
    public class DoctorPostRequestTest : IClassFixture<HospitalWebAppFactory<Program>>
    {
        private readonly HospitalWebAppFactory<Program> _factory;

        public DoctorPostRequestTest(HospitalWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task AddDoctor_ReturnsCreatedResult_WithAddedDoctorId()
        {
            var client = _factory.CreateClient();

            var newDoctorDto = new DoctorRequestDto
            {
                Name = "Buck",
                Surname = "Stevens",
                Address = "Cahul, Moldova",
                PhoneNumber = "078954896",
                DepartmentId = 1,
                StartShift = "12:30:00",
                EndShift = "20:00:00",
                WeekDayIds = new List<int> { 2, 4, 6 }
            };
            var content = new StringContent(JsonSerializer.Serialize(newDoctorDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Doctor", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var doctorId = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(3, doctorId);
        }

        [Fact]
        public async Task AddDoctor_ReturnsNotFoundResult_DepartmentDoesNotExist()
        {
            var client = _factory.CreateClient();

            var newDoctorDto = new DoctorRequestDto
            {
                Name = "Buck",
                Surname = "Stevens",
                Address = "Cahul, Moldova",
                PhoneNumber = "078954896",
                DepartmentId = 2,
                StartShift = "12:30:00",
                EndShift = "20:00:00",
                WeekDayIds = new List<int> { 2, 4, 6 }
            };
            var content = new StringContent(JsonSerializer.Serialize(newDoctorDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Doctor", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddDoctor_ReturnsBadRequestResult_InvalidRequestModel()
        {
            var client = _factory.CreateClient();

            var newDoctorDto = new DoctorRequestDto
            {
                Name = "Buck",
                Surname = "Stevens",
                Address = "Cahul, Moldova",
                PhoneNumber = "078954896",
                DepartmentId = 2,
                StartShift = "12:30:00:00",
                EndShift = "20:00:00",
                WeekDayIds = new List<int> { 2, 4, 6 }
            };
            var content = new StringContent(JsonSerializer.Serialize(newDoctorDto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/Doctor", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SearchDoctorsByASetOfProperties_ReturnsOkResult_WithListOfDoctorFullInfoDtos()
        {
            var client = _factory.CreateClient();

            var filter = new DoctorFilterRequestDto
            {
                Name = "Greg",
            };

            var response = await client.PostAsJsonAsync("/api/Doctor/Search", filter);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var doctors = await response.Content.ReadFromJsonAsync<List<DoctorFullInfoDto>>();
            Assert.NotNull(doctors);
            Assert.Single(doctors);
        }

        [Fact]
        public async Task SearchDoctorsByASetOfProperties_ReturnsNotFoundResult()
        {
            var client = _factory.CreateClient();

            var filter = new DoctorFilterRequestDto
            {
                Name = "Greg",
                DepartmentName = "Oncology"
            };

            var response = await client.PostAsJsonAsync("/api/Doctor/Search", filter);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
