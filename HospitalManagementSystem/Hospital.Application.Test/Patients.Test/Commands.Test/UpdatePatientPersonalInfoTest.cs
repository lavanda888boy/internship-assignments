using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Patients.Test.Commands.Test
{
    public class UpdatePatientPersonalInfoTest
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;

        public UpdatePatientPersonalInfoTest()
        {
            _patientRepositoryMock = new();
        }

        [Theory]
        [MemberData(nameof(UpdatePatientPersonalInfoData))]
        public void Handle_ShouldReturnPatientDtoWithUpdatedPersonalInfo(Patient patient)
        {
            _patientRepositoryMock.Setup(repo => repo.GetById(patient.Id)).Returns(patient);
            _patientRepositoryMock.Setup(repo => repo.Update(It.IsAny<Patient>())).Returns(true);

            var command = new UpdatePatientPersonalInfo(patient.Id, patient.Name, patient.Surname,
                patient.Age, patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new UpdatePatientPersonalInfoHandler(_patientRepositoryMock.Object);

            Task<PatientDto> patientDto = handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.Equal(patient.Name, patientDto.Result.Name);
                Assert.Equal(patient.Address, patientDto.Result.Address);
                Assert.Equal(patient.Age, patientDto.Result.Age);
                Assert.NotNull(patientDto.Result.InsuranceNumber);
            });

            _patientRepositoryMock.Verify(repo => repo.GetById(patient.Id), Times.Once);
            _patientRepositoryMock.Verify(repo => repo.Update(It.IsAny<Patient>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UpdatePatientPersonalInfoData))]
        public async Task Handle_ShouldThrowException_PatientDoesNotExist(Patient patient)
        {
            _patientRepositoryMock.Setup(repo => repo.GetById(patient.Id));

            var command = new UpdatePatientPersonalInfo(patient.Id, patient.Name, patient.Surname,
                patient.Age, patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new UpdatePatientPersonalInfoHandler(_patientRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(async () => await handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetById(patient.Id), Times.Once);
        }

        public static TheoryData<Patient> UpdatePatientPersonalInfoData => new TheoryData<Patient>()
        {
            {
                new Patient()
                {
                    Id = 1,
                    Name = "Sevastian",
                    Surname = "Bajenov",
                    Gender = "M",
                    Address = "Dacia 29",
                    Age = 21,
                    PhoneNumber = "085964712",
                    InsuranceNumber = "AB78945"
                }
            }
        };
    }
}
