using Hospital.Application.Abstractions;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Patients.Responses;

namespace Hospital.Application.Test.Patients.Test.Queries.Test
{
    public class ListAllPatientsTest
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;

        public ListAllPatientsTest()
        {
            _patientRepositoryMock = new();
        }

        [Fact]
        public void Handle_ShouldReturnListOfPatientDtos()
        {
            var patients = new List<Patient>
            {
                new Patient()
                {
                    Id = 1,
                    Name = "Seva",
                    Surname = "Bajenov",
                    Gender = "M",
                    Address = "Dacia 24",
                    Age = 20,
                    PhoneNumber = "085964712",
                    AssignedDoctors = new List<Doctor>
                    {
                        new Doctor()
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
                            WorkingHours = new DoctorWorkingHours()
                            {
                                Id = 1,
                                StartShift = new TimeSpan(7, 0, 0),
                                EndShift = new TimeSpan(16, 0, 0),
                                WeekDays = new List<DayOfWeek> {DayOfWeek.Monday},
                            }
                        }
                    }
                },
                new Patient()
                {
                    Id = 2,
                    Name = "Mary",
                    Surname = "Smith",
                    Gender = "F",
                    Address = "California",
                    Age = 35,
                    PhoneNumber = "045698745",
                    AssignedDoctors = new List<Doctor>
                    {
                        new Doctor()
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
                            WorkingHours = new DoctorWorkingHours()
                            {
                                Id = 1,
                                StartShift = new TimeSpan(7, 0, 0),
                                EndShift = new TimeSpan(16, 0, 0),
                                WeekDays = new List<DayOfWeek> {DayOfWeek.Monday},
                            }
                        }
                    }
                }
            };

            _patientRepositoryMock.Setup(repo => repo.GetAll()).Returns(patients);

            var command = new ListAllPatients();
            var handler = new ListAllPatientsHandler(_patientRepositoryMock.Object);

            Task<List<PatientDto>> patientDtos = handler.Handle(command, default);

            _patientRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.Equal(2, patientDtos.Result.Count);
                Assert.Equal("Seva", patientDtos.Result[0].Name);
                Assert.Single(patientDtos.Result[0].AssignedDoctors);
                Assert.Equal(35, patientDtos.Result[1].Age);
            });
        }
    }
}
