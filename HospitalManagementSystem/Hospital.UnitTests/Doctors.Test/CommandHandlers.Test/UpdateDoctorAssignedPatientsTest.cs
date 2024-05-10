using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Doctors.Commands;

namespace Hospital.UnitTests.Doctors.Test.CommandHandlers.Test
{
    public class UpdateDoctorAssignedPatientsTest
    {
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;

        public UpdateDoctorAssignedPatientsTest()
        {
            _doctorRepositoryMock = new();
        }

        [Fact]
        public async Task UpdateDoctorAssignedPatients_ShouldReturnUpdatedDoctorId()
        {
            var doctor = new Doctor()
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
            };

            var patientIds = new List<int>() { 1, 3, 4 };

            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));
            _doctorRepositoryMock.Setup(repo => repo.UpdateAsync(doctor));

            var command = new UpdateDoctorAssignedPatients(doctor.Id, patientIds);
            var handler = new UpdateDoctorAssignedPatientsHandler(_doctorRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(doctor.Id, result);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Doctor>(d =>
                d.DoctorsPatients.Any(dp => dp.PatientId == patientIds[0])
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateDoctorAssignedPatients_ShouldThrowException_DoctorToUpdateDoesNotExist()
        {
            int doctorId = 2;
            var patientIds = new List<int>() { 1, 3, 4 };

            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctorId));

            var command = new UpdateDoctorAssignedPatients(doctorId, patientIds);
            var handler = new UpdateDoctorAssignedPatientsHandler(_doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctorId), Times.Once);
        }
    }
}
