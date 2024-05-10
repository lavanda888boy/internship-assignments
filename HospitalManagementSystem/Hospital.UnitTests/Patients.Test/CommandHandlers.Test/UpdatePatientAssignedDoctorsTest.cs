using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Abstractions;

namespace Hospital.UnitTests.Patients.Test.CommandHandlers.Test
{
    public class UpdatePatientAssignedDoctorsTest
    {
        private Mock<IRepository<Patient>> _patientRepositoryMock;

        public UpdatePatientAssignedDoctorsTest()
        {
            _patientRepositoryMock = new();
        }

        [Fact]
        public async Task UpdatePatientAssignedDoctors_ShouldReturnUpdatedPatientId()
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
            var doctorIds = new List<int>() { 1, 3, 4 };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _patientRepositoryMock.Setup(repo => repo.UpdateAsync(patient));

            var command = new UpdatePatientAssignedDoctors(patient.Id, doctorIds);
            var handler = new UpdatePatientAssignedDoctorsHandler(_patientRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(patient.Id, result);
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
            _patientRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Patient>(p =>
                p.DoctorsPatients.Any(dp => dp.DoctorId == doctorIds[0])
            )), Times.Once);
        }

        [Fact]
        public async Task UpdatePatientAssignedDoctors_ShouldThrowException_PatientToUpdateDoesNotExist()
        {
            int patientId = 2;
            var doctorIds = new List<int>() { 1, 3, 4 };

            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId));

            var command = new UpdatePatientAssignedDoctors(patientId, doctorIds);
            var handler = new UpdatePatientAssignedDoctorsHandler(_patientRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patientId), Times.Once);
        }
    }
}
