using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;
using System.Linq.Expressions;

namespace Hospital.UnitTests.Patients.Test.QueryHandlers.Test
{
    public class SearchPatientsByASetOfPropertiesTest
    {
        private Mock<IRepository<Patient>> _patientRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public SearchPatientsByASetOfPropertiesTest()
        {
            _patientRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task SearchPatientsByASetOfProperties_ShouldReturnListOfPatientFullInfoDtos()
        {
            var patients = new List<Patient>()
            {
                new()
                {
                    Id = 1,
                    Name = "Mike",
                    Surname = "Smith",
                    Age = 25,
                    Gender = Gender.M,
                    Address = "Some address",
                    PhoneNumber = "068749856",
                    InsuranceNumber = "AB45687952"
                }
            };

            _patientRepositoryMock.Setup(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                .Returns(Task.FromResult(patients));

            _mapperMock.Setup(mapper => mapper.Map<List<PatientFullInfoDto>>(patients))
                .Returns(new List<PatientFullInfoDto>()
                {
                    new()
                    {
                        Id = patients[0].Id,
                        Name = patients[0].Name,
                        Surname = patients[0].Surname,
                        Age = patients[0].Age,
                        Gender = patients[0].Gender.ToString(),
                        Address = patients[0].Address,
                        PhoneNumber = patients[0].PhoneNumber,
                        InsuranceNumber = patients[0].InsuranceNumber,
                        Doctors = new List<DoctorShortInfoDto>()
                        {
                            new()
                            {
                                FullName = "Michael Bay",
                                Department = "Heart diseases"
                            }
                        }
                    }
                });

            var command = new SearchPatientsByASetOfProperties("Mike", "", 25, "", "", "", "");
            var handler = new SearchPatientsByASetOfPropertiesHandler(_patientRepositoryMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(patients.Count, result.Count);
            Assert.Equal(patients[0].Id, result[0].Id);
            _patientRepositoryMock.Verify(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<Patient, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task SearchPatientsByASetOfProperties_ShouldThrowException_PatientsWithSuchPropertiesDoNotExist()
        {
            var patients = new List<Patient>();

            _patientRepositoryMock.Setup(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<Patient, bool>>>()))
                .Returns(Task.FromResult(patients));

            var command = new SearchPatientsByASetOfProperties("Mike", "", 25, "", "", "", "");
            var handler = new SearchPatientsByASetOfPropertiesHandler(_patientRepositoryMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<Patient, bool>>>()), Times.Once);
        }
    }
}
