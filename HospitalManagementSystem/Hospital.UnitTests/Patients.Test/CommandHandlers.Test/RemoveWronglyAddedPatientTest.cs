using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.Patients.Test.CommandHandlers.Test
{
    public class RemoveWronglyAddedPatientTest
    {
        private Mock<IRepository<Patient>> _patientRepositoryMock;

        public RemoveWronglyAddedPatientTest()
        {
            _patientRepositoryMock = new();
        }

        [Fact]
        public async Task RemoveWronglyAddedPatient_ShouldReturnRemovedPatientId()
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
            _patientRepositoryMock.Setup(repo => repo.DeleteAsync(patient));

            var command = new RemoveWronglyAddedPatient(patient.Id);
            var handler = new RemoveWronglyAddedPatientHandler(_patientRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(patient.Id, result);
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _patientRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Patient>()), Times.Once);
        }

        [Fact]
        public async Task RemoveWronglyAddedPatient_ShouldThrowException_PatientToDeleteDoesNotExist()
        {
            int patientId = 2;
            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId));

            var command = new RemoveWronglyAddedPatient(patientId);
            var handler = new RemoveWronglyAddedPatientHandler(_patientRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patientId), Times.Once);
        }
    }
}
