using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.Patients.Test.CommandHandlers.Test
{
    public class RegisterNewPatientTest
    {
        private Mock<IRepository<Patient>> _patientRepositoryMock;
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;

        public RegisterNewPatientTest()
        {
            _patientRepositoryMock = new();
            _doctorRepositoryMock = new();
        }

        [Fact]
        public async Task RegisterNewPatient_ShouldReturnPatientId()
        {
            var patient = new Patient()
            {
                Id = 1,
                Name = "Steve",
                Surname = "Smith",
                Age = 25,
                Gender = Gender.M,
                Address = "Some address",
                PhoneNumber = "068749856",
                InsuranceNumber = "AB45687952"
            };

            var doctors = new List<Doctor>() {
                new()
                {
                    Id = 1,
                    Name = "Mick",
                    Surname = "Mouse",
                    Address = "Chisinau",
                    PhoneNumber = "079854623",
                    Department = new Department()
                    {
                        Id = 1,
                        Name = "Heart diseases",
                    },
                    WorkingHours = new DoctorSchedule()
                    {
                        Id = 1,
                        StartShift = new TimeSpan(7, 0, 0),
                        EndShift = new TimeSpan(16, 0, 0),
                    }
                }
            };

            _patientRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Patient>())).Returns(Task.FromResult(patient));
            _doctorRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(doctors));

            var command = new RegisterNewPatient(patient.Name, patient.Surname, patient.Age, patient.Gender.ToString(),
                patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new RegisterNewPatientHandler(_patientRepositoryMock.Object, 
                _doctorRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(patient.Id, result);
            _doctorRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _patientRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Patient>()), Times.Once);
        }

        [Fact]
        public async Task RegisterNewPatient_ShouldThrowException_ThereAreNoDoctorsAvailable()
        {
            var patient = new Patient()
            {
                Id = 1,
                Name = "Steve",
                Surname = "Smith",
                Age = 25,
                Gender = Gender.M,
                Address = "Some address",
                PhoneNumber = "068749856",
                InsuranceNumber = "AB45687952"
            };

            var doctors = new List<Doctor>();
            _doctorRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(doctors));

            var command = new RegisterNewPatient(patient.Name, patient.Surname, patient.Age, patient.Gender.ToString(),
                patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new RegisterNewPatientHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
