using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Commands.Test
{
    public class AddNewRegularMedicalRecordTest
    {
        private readonly Mock<IRegularMedicalRecordRepository> _medicalRecordRepositoryMock;
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

        public AddNewRegularMedicalRecordTest()
        {
            _medicalRecordRepositoryMock = new();
            _patientRepositoryMock = new();
            _doctorRepositoryMock = new();
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ok")]
        public void Handle_ShouldReturnRegularMedicalRecordDto(int id, int patientId, int doctorId,
            string notes)
        {
            var patient = new Patient()
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
            };

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
                WorkingHours = new DoctorWorkingHours()
                {
                    Id = 1,
                    StartShift = new TimeSpan(7, 0, 0),
                    EndShift = new TimeSpan(16, 0, 0),
                    WeekDays = new List<DayOfWeek> { DayOfWeek.Monday },
                }
            };

            var record = new RegularMedicalRecord()
            {
                Id = id,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes
            };

            _patientRepositoryMock.Setup(repo => repo.GetById(patientId)).Returns(patient);
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctorId)).Returns(doctor);
            _medicalRecordRepositoryMock.Setup(repo => repo.Create(It.IsAny<RegularMedicalRecord>())).Returns(record);

            var command = new AddNewRegularMedicalRecord(id, patientId, doctorId, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object);

            Task<RegularMedicalRecordDto> recordDto = handler.Handle(command, default);

            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctorId), Times.Once);
            _medicalRecordRepositoryMock.Verify(repo => repo.Create(It.IsAny<RegularMedicalRecord>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Equal(id, recordDto.Result.Id);
                Assert.Equal(patient.Id, recordDto.Result.ExaminedPatient.Id);
                Assert.Equal(doctor.Id, recordDto.Result.ResponsibleDoctor.Id);
                Assert.Equal(notes, recordDto.Result.ExaminationNotes);
            });
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ok")]
        public async Task Handle_ShouldThrowException_PatientDoesNotExist(int id, int patientId, int doctorId,
            string notes)
        {
            _patientRepositoryMock.Setup(repo => repo.GetById(patientId));

            var command = new AddNewRegularMedicalRecord(id, patientId, doctorId, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ok")]
        public async Task Handle_ShouldThrowException_DoctorDoesNotExist(int id, int patientId, int doctorId,
            string notes)
        {
            var patient = new Patient()
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
            };

            _patientRepositoryMock.Setup(repo => repo.GetById(patientId)).Returns(patient);
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctorId));

            var command = new AddNewRegularMedicalRecord(id, patientId, doctorId, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctorId), Times.Once);
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ok")]
        public async void Handle_ShouldThrowException_PatientIsNotAssignedToTheDoctor(int id, int patientId, int doctorId,
            string notes)
        {
            var patient = new Patient()
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
                        Id = 2,
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
            };

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
                WorkingHours = new DoctorWorkingHours()
                {
                    Id = 1,
                    StartShift = new TimeSpan(7, 0, 0),
                    EndShift = new TimeSpan(16, 0, 0),
                    WeekDays = new List<DayOfWeek> { DayOfWeek.Monday },
                }
            };

            var record = new RegularMedicalRecord()
            {
                Id = id,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes
            };

            _patientRepositoryMock.Setup(repo => repo.GetById(patientId)).Returns(patient);
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctorId)).Returns(doctor);

            var command = new AddNewRegularMedicalRecord(id, patientId, doctorId, notes);
            var handler = new AddNewRegularMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object);

            await Assert.ThrowsAsync<PatientDoctorMisassignationException>(() => handler.Handle(command, default));

            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctorId), Times.Once);
        }
    }
}
