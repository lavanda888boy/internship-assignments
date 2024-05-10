using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.Patients.Test.QueryHandlers.Test
{
    public class GetPatientByIdTest
    {
        private Mock<IRepository<Patient>> _patientRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public GetPatientByIdTest()
        {
            _patientRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task GetPatientById_ShouldReturnPatientFullInfoDto()
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

            var patientDto = new PatientFullInfoDto()
            {
                Id = 1,
                Name = "Steve",
                Surname = "Smith",
                Age = 25,
                Gender = "M",
                Address = "Some address",
                PhoneNumber = "068749856",
                InsuranceNumber = "AB45687952",
                Doctors = new List<DoctorShortInfoDto>()
                {
                    new()
                    {
                        FullName = "Michael Bay",
                        Department = "Heart diseases"
                    }
                }
            };
            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patient.Id)).Returns(Task.FromResult(patient));
            _mapperMock.Setup(mapper => mapper.Map<PatientFullInfoDto>(patient))
                .Returns(new PatientFullInfoDto()
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Surname = patient.Surname,
                    Age = patient.Age,
                    Gender = patient.Gender.ToString(),
                    Address = patient.Address,
                    PhoneNumber = patient.PhoneNumber,
                    InsuranceNumber = patient.InsuranceNumber,
                    Doctors = new List<DoctorShortInfoDto>()
                    {
                        new()
                        {
                            FullName = "Michael Bay",
                            Department = "Heart diseases"
                        }
                    }
                });

            var command = new GetPatientById(patient.Id);
            var handler = new GetPatientByIdHandler(_patientRepositoryMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(patientDto.Id, result.Id);
            Assert.Equal(patientDto.Doctors.Count, result.Doctors.Count);
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patient.Id), Times.Once);
        }

        [Fact]
        public async Task GetPatientById_ShouldThrowException_PatientDoesNotExist()
        {
            int patientId = 2;
            _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId));

            var command = new GetPatientById(patientId);
            var handler = new GetPatientByIdHandler(_patientRepositoryMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetByIdAsync(patientId), Times.Once);
        }
    }
}
