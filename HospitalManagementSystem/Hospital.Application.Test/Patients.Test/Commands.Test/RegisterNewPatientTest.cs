using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.Patients.Commands;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.Patients.Test.Commands.Test
{
    public class RegisterNewPatientTest
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private Mock<IDoctorRepository> _doctorRepositoryMock;

        public RegisterNewPatientTest()
        {
            _patientRepositoryMock = new();
            _doctorRepositoryMock = new();
        }

        [Theory]
        [MemberData(nameof(RegisterNewPatientData))]
        public void Handle_ShouldReturnNewlyRegisteredPatient(Patient patient)
        {
            var doctors = new List<Doctor>()
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
            };

            _patientRepositoryMock.Setup(repo => repo.Create(It.IsAny<Patient>())).Returns(patient);
            _doctorRepositoryMock.Setup(repo => repo.GetAll()).Returns(doctors);
            _doctorRepositoryMock.Setup(repo => repo.Update(It.IsAny<Doctor>())).Returns(true);

            var command = new RegisterNewPatient(patient.Id, patient.Name, patient.Surname,
                patient.Age, patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new RegisterNewPatientHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object);

            Task<PatientDto> patientDto = handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.Equal(patient.Name, patientDto.Result.Name);
                Assert.Equal(patient.Address, patientDto.Result.Address);
                Assert.Equal(patient.PhoneNumber, patientDto.Result.PhoneNumber);
            });

            _patientRepositoryMock.Verify(repo => repo.Create(It.IsAny<Patient>()), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.Update(It.IsAny<Doctor>()), Times.Once);

            Assert.Equal(patientDto.Result.Id, doctors[0].AssignedPatients.ToList()[0].Id);
        }

        [Theory]
        [MemberData(nameof(RegisterNewPatientData))]
        public async Task Handle_ShouldThrowExceptionIfThereAreNoDoctorsAvailable(Patient patient)
        {
            var doctors = new List<Doctor>();
            _doctorRepositoryMock.Setup(repo => repo.GetAll()).Returns(doctors);

            var command = new RegisterNewPatient(patient.Id, patient.Name, patient.Surname,
                patient.Age, patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new RegisterNewPatientHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RegisterNewPatientData))]
        public async Task Handle_ShouldThrowException_DoctorHasMaximumNumberOfPatients(Patient patient)
        {
            var doctors = new List<Doctor>()
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
                    AssignedPatients = new List<Patient>()
                    {
                        new Patient()
                        {
                            Id = 2,
                            Name = "Seva",
                            Surname = "Bajenov",
                            Gender = "M",
                            Address = "Dacia 24",
                            Age = 20,
                            PhoneNumber = "085964712",
                            InsuranceNumber = null
                        },
                        new Patient()
                        {
                            Id = 3,
                            Name = "Seva",
                            Surname = "Bajenov",
                            Gender = "M",
                            Address = "Dacia 24",
                            Age = 20,
                            PhoneNumber = "085964712",
                            InsuranceNumber = null
                        }
                    },
                    WorkingHours = new DoctorWorkingHours()
                    {
                        Id = 1,
                        StartShift = new TimeSpan(7, 0, 0),
                        EndShift = new TimeSpan(16, 0, 0),
                        WeekDays = new List<DayOfWeek> {DayOfWeek.Monday},
                    }
                }
            };
            _doctorRepositoryMock.Setup(repo => repo.GetAll()).Returns(doctors);

            var command = new RegisterNewPatient(patient.Id, patient.Name, patient.Surname,
                patient.Age, patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new RegisterNewPatientHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(RegisterNewPatientData))]
        public async Task Handle_ShouldThrowException_PatientIsAlreadyAssignedToTheDoctor(Patient patient)
        {
            var doctors = new List<Doctor>()
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
                    AssignedPatients = new List<Patient>()
                    {
                        patient
                    },
                    WorkingHours = new DoctorWorkingHours()
                    {
                        Id = 1,
                        StartShift = new TimeSpan(7, 0, 0),
                        EndShift = new TimeSpan(16, 0, 0),
                        WeekDays = new List<DayOfWeek> {DayOfWeek.Monday},
                    }
                }
            };
            _doctorRepositoryMock.Setup(repo => repo.GetAll()).Returns(doctors);

            var command = new RegisterNewPatient(patient.Id, patient.Name, patient.Surname,
                patient.Age, patient.Gender, patient.Address, patient.PhoneNumber, patient.InsuranceNumber);
            var handler = new RegisterNewPatientHandler(_patientRepositoryMock.Object,
                _doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _doctorRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        public static TheoryData<Patient> RegisterNewPatientData => new TheoryData<Patient>()
        {
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
                    InsuranceNumber = null
                }
            }
        };

    }
}
