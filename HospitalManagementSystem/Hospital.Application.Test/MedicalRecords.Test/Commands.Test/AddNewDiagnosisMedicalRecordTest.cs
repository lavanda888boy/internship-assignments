using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Commands.Test
{
    public class AddNewDiagnosisMedicalRecordTest
    {
        private readonly Mock<IDiagnosisMedicalRecordRepository> _medicalRecordRepositoryMock;
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly Mock<IIllnessRepository> _illnessRepositoryMock;
        private readonly Mock<ITreatmentRepository> _treatmentRepositoryMock;

        public AddNewDiagnosisMedicalRecordTest()
        {
            _medicalRecordRepositoryMock = new();
            _patientRepositoryMock = new();
            _doctorRepositoryMock = new();
            _illnessRepositoryMock = new();
            _treatmentRepositoryMock = new();
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ill", 1, 1, "panadol", 5)]
        public void Handle_ShouldReturnDiagnosisMedicalRecordDto(int id, int patientId, int doctorId,
            string notes, int illnessId, int treatmentId, string medicine, int days)
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

            var illness = new Illness { Id = 1, Name = "flu", IllnessSeverity = IllnessSeverity.MEDIUM };

            var record = new DiagnosisMedicalRecord()
            {
                Id = id,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes,
                DiagnosedIllness = illness,
                ProposedTreatment = new Treatment()
                {
                    Id = treatmentId,
                    PrescribedMedicine = medicine,
                    TreatmentDuration = new TimeSpan(days, 0, 0, 0)
                }
            };

            _patientRepositoryMock.Setup(repo => repo.GetById(patientId)).Returns(patient);
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctorId)).Returns(doctor);
            _illnessRepositoryMock.Setup(repo => repo.GetById(illnessId)).Returns(illness);
            _treatmentRepositoryMock.Setup(repo => repo.Create(It.IsAny<Treatment>())).Returns(record.ProposedTreatment);
            _medicalRecordRepositoryMock.Setup(repo => repo.Create(It.IsAny<DiagnosisMedicalRecord>())).Returns(record);

            var command = new AddNewDiagnosisMedicalRecord(id, patientId, doctorId, notes, illnessId, treatmentId, medicine, new TimeSpan(days, 0, 0, 0));
            var handler = new AddNewDiagnosisMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object, _illnessRepositoryMock.Object,
                _treatmentRepositoryMock.Object);

            Task<DiagnosisMedicalRecordDto> recordDto = handler.Handle(command, default);

            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctorId), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetById(illnessId), Times.Once);
            _treatmentRepositoryMock.Verify(repo => repo.Create(It.IsAny<Treatment>()), Times.Once);
            _medicalRecordRepositoryMock.Verify(repo => repo.Create(It.IsAny<DiagnosisMedicalRecord>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.Equal(id, recordDto.Result.Id);
                Assert.Equal(patient.Id, recordDto.Result.ExaminedPatient.Id);
                Assert.Equal(doctor.Id, recordDto.Result.ResponsibleDoctor.Id);
                Assert.Equal(notes, recordDto.Result.ExaminationNotes);
                Assert.Equal(illnessId, recordDto.Result.DiagnosedIllness.Id);
                Assert.Equal(treatmentId, recordDto.Result.ProposedTreatment.Id);
            });
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ill", 1, 1, "panadol", 5)]
        public async Task Handle_ShouldThrowException_PatientDoesNotExist(int id, int patientId, int doctorId,
            string notes, int illnessId, int treatmentId, string medicine, int days)
        {
            _patientRepositoryMock.Setup(repo => repo.GetById(patientId));

            var command = new AddNewDiagnosisMedicalRecord(id, patientId, doctorId, notes, illnessId,
                treatmentId, medicine, new TimeSpan(days, 0, 0, 0));
            var handler = new AddNewDiagnosisMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object, _illnessRepositoryMock.Object,
                _treatmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
        }
        
        [Theory]
        [InlineData(1, 1, 1, "patient is ill", 1, 1, "panadol", 5)]
        public async Task Handle_ShouldThrowException_DoctorDoesNotExist(int id, int patientId, int doctorId,
            string notes, int illnessId, int treatmentId, string medicine, int days)
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

            var command = new AddNewDiagnosisMedicalRecord(id, patientId, doctorId, notes, illnessId,
                treatmentId, medicine, new TimeSpan(days, 0, 0, 0));
            var handler = new AddNewDiagnosisMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object, _illnessRepositoryMock.Object,
                _treatmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctorId), Times.Once);
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ill", 1, 1, "panadol", 5)]
        public async Task Handle_ShouldThrowException_IllnessDoesNotExist(int id, int patientId, int doctorId,
            string notes, int illnessId, int treatmentId, string medicine, int days)
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

            _patientRepositoryMock.Setup(repo => repo.GetById(patientId)).Returns(patient);
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctorId)).Returns(doctor);
            _illnessRepositoryMock.Setup(repo => repo.GetById(illnessId));

            var command = new AddNewDiagnosisMedicalRecord(id, patientId, doctorId, notes, illnessId,
                treatmentId, medicine, new TimeSpan(days, 0, 0, 0));
            var handler = new AddNewDiagnosisMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object, _illnessRepositoryMock.Object,
                _treatmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctorId), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetById(illnessId), Times.Once);
        }

        [Theory]
        [InlineData(1, 1, 1, "patient is ill", 1, 1, "panadol", 5)]
        public async Task Handle_ShouldThrowException_PatientIsNotAssignedToTheDoctor(int id, int patientId, int doctorId,
            string notes, int illnessId, int treatmentId, string medicine, int days)
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
                    WeekDays = new List<DayOfWeek> { DayOfWeek.Monday },
                }
            };

            var illness = new Illness { Id = 1, Name = "flu", IllnessSeverity = IllnessSeverity.MEDIUM };

            _patientRepositoryMock.Setup(repo => repo.GetById(patientId)).Returns(patient);
            _doctorRepositoryMock.Setup(repo => repo.GetById(doctorId)).Returns(doctor);
            _illnessRepositoryMock.Setup(repo => repo.GetById(illnessId)).Returns(illness);

            var command = new AddNewDiagnosisMedicalRecord(id, patientId, doctorId, notes, illnessId,
                treatmentId, medicine, new TimeSpan(days, 0, 0, 0));
            var handler = new AddNewDiagnosisMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _patientRepositoryMock.Object, _doctorRepositoryMock.Object, _illnessRepositoryMock.Object,
                _treatmentRepositoryMock.Object);

            await Assert.ThrowsAsync<PatientDoctorMisassignationException>(() => handler.Handle(command, default));
            _patientRepositoryMock.Verify(repo => repo.GetById(patientId), Times.Once);
            _doctorRepositoryMock.Verify(repo => repo.GetById(doctorId), Times.Once);
            _illnessRepositoryMock.Verify(repo => repo.GetById(illnessId), Times.Once);
        }
    }
}
