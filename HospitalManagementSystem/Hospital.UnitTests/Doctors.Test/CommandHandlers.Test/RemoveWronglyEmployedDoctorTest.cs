using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Commands;
using Hospital.Application.Exceptions;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.UnitTests.Doctors.Test.CommandHandlers.Test
{
    public class RemoveWronglyEmployedDoctorTest
    {
        private Mock<IRepository<Doctor>> _doctorRepositoryMock;

        public RemoveWronglyEmployedDoctorTest()
        {
            _doctorRepositoryMock = new();
        }

        [Fact]
        public async Task RemoveWronglyEmployedDoctor_ShouldReturnRemovedDoctorId()
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

            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctor.Id)).Returns(Task.FromResult(doctor));

            var command = new RemoveWronglyEmployedDoctor(doctor.Id);
            var handler = new RemoveWronglyEmployedDoctorHandler(_doctorRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(doctor.Id, result);
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Doctor>()), Times.Once);
        }

        [Fact]
        public async Task RemoveWronglyEmployedDoctor_ShouldThrowException_DoctorToRemoveDoesNotExist()
        {
            int doctorId = 2;
            _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(doctorId));

            var command = new RemoveWronglyEmployedDoctor(doctorId);
            var handler = new RemoveWronglyEmployedDoctorHandler(_doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetByIdAsync(doctorId), Times.Once);
        }
    }
}
