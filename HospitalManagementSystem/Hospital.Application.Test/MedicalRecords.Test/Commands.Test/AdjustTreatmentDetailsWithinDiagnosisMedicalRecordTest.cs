using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.Application.Test.MedicalRecords.Test.Commands.Test
{
    public class AdjustTreatmentDetailsWithinDiagnosisMedicalRecordTest
    {
        private readonly Mock<IDiagnosisMedicalRecordRepository> _medicalRecordRepositoryMock;
        private readonly Mock<IIllnessRepository> _illnessRepositoryMock;
        private readonly Mock<ITreatmentRepository> _treatmentRepositoryMock;

        public AdjustTreatmentDetailsWithinDiagnosisMedicalRecordTest()
        {
            _medicalRecordRepositoryMock = new();
            _illnessRepositoryMock = new();
            _treatmentRepositoryMock = new();
        }

        [Theory]
        [InlineData(1, 1, "panadol", 5)]
        public void Handle_ShouldReturnUpdatedMedicalRecordDto(int id, int illnessId, 
            string medicine, int days)
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

            var record = new DiagnosisMedicalRecord()
            {
                Id = id,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = "some notes",
                DiagnosedIllness = new Illness()
                {
                    Id = illnessId,
                    Name = "allergy",
                    IllnessSeverity = IllnessSeverity.MEDIUM,
                },
                ProposedTreatment = new Treatment()
                {
                    Id = 1,
                    PrescribedMedicine = medicine,
                    TreatmentDuration = new TimeSpan(days, 0, 0, 0)
                }
            };

            _medicalRecordRepositoryMock.Setup(repo => repo.GetById(id)).Returns(record);
            _medicalRecordRepositoryMock.Setup(repo => repo.Update(It.IsAny<DiagnosisMedicalRecord>())).Returns(true);
            _illnessRepositoryMock.Setup(repo => repo.GetById(illnessId)).Returns(record.DiagnosedIllness);
            _treatmentRepositoryMock.Setup(repo => repo.Update(It.IsAny<Treatment>())).Returns(true);
            _medicalRecordRepositoryMock.Setup(repo => repo.Update(It.IsAny<DiagnosisMedicalRecord>())).Returns(true);

            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(id, illnessId, medicine, record.ProposedTreatment.TreatmentDuration);
            var handler = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _illnessRepositoryMock.Object, _treatmentRepositoryMock.Object);

            Task<DiagnosisMedicalRecordDto> recordDto = handler.Handle(command, default);

            Assert.Multiple(() =>
            {
                Assert.Equal(id, recordDto.Result.Id);
                Assert.Equal(medicine, recordDto.Result.ProposedTreatment.PrescribedMedicine);
                Assert.Equal("allergy", recordDto.Result.DiagnosedIllness.Name);
            });

            _medicalRecordRepositoryMock.Verify(repo => repo.GetById(id), Times.Once);
            _medicalRecordRepositoryMock.Verify(repo => repo.Update(It.IsAny<DiagnosisMedicalRecord>()), Times.Once);
            _illnessRepositoryMock.Setup(repo => repo.GetById(illnessId)).Returns(record.DiagnosedIllness);
            _treatmentRepositoryMock.Setup(repo => repo.Update(It.IsAny<Treatment>())).Returns(true);
            _medicalRecordRepositoryMock.Setup(repo => repo.Update(It.IsAny<DiagnosisMedicalRecord>())).Returns(true);
        }

        [Theory]
        [InlineData(1, 1, "panadol", 5)]
        public async Task Handle_ShouldThrowException_RegularMedicalRecordDoesNotExist(int id, int illnessId,
            string medicine, int days)
        {
            _medicalRecordRepositoryMock.Setup(repo => repo.GetById(id));

            var command = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecord(id, illnessId, medicine, new TimeSpan(days, 0, 0, 0));
            var handler = new AdjustTreatmentDetailsWithinDiagnosisMedicalRecordHandler(_medicalRecordRepositoryMock.Object,
                _illnessRepositoryMock.Object, _treatmentRepositoryMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _medicalRecordRepositoryMock.Verify(repo => repo.GetById(id), Times.Once);
        }
    }
}
