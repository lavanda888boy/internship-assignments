using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.Patients.Test.CommandHandlers.Test
{
    public class UpdatePatientPersonalInfoTest
    {
        private Mock<IRepository<Patient>> _patientRepositoryMock;

        public UpdatePatientPersonalInfoTest()
        {
            _patientRepositoryMock = new();
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ShouldReturnUpdatedPatientId()
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

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _patientRepositoryMock.Setup(repo => repo.UpdateAsync(patient));

            var command = new UpdatePatientPersonalInfo(patient.Id, "Mike", patient.Surname, patient.Age,
                patient.Gender.ToString(), "Another address", patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new UpdatePatientPersonalInfoHandler(_patientRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(patient.Id, result);
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _patientRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Patient>(p => 
                p.Name == "Mike" &&
                p.Address == "Another address"
            )), Times.Once);
        }

        [Fact]
        public async Task UpdatePatientPersonalInfo_ShouldThrowException_PatientToUpdateDoesNotExist()
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

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id));

            var command = new UpdatePatientPersonalInfo(patient.Id, "Mike", patient.Surname, patient.Age,
                patient.Gender.ToString(), "Another address", patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new UpdatePatientPersonalInfoHandler(_patientRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
        }
    }
}
