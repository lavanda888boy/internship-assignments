using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;
using Hospital.Application.Illnesses.Responses;
using Hospital.Application.Treatments.Responses;

namespace Hospital.UnitTests.MedicalRecords.Test.QueryHandlers.Test
{
    public class GetDiagnosisMedicalRecordByIdTest
    {
        private Mock<IRepository<DiagnosisMedicalRecord>> _recordRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public GetDiagnosisMedicalRecordByIdTest()
        {
            _recordRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task GetDiagnosisMedicalRecordById_ShouldReturnMedicalRecordFullInfoDto()
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
                InsuranceNumber = "AB45687952",
                DoctorsPatients = new List<DoctorsPatients>()
                {
                    new DoctorsPatients()
                    {
                        PatientId = 1,
                        DoctorId = 1
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
                WorkingHours = new DoctorSchedule()
                {
                    Id = 1,
                    StartShift = new TimeSpan(7, 0, 0),
                    EndShift = new TimeSpan(16, 0, 0),
                },
                DoctorsPatients = new List<DoctorsPatients>()
                {
                    new DoctorsPatients()
                    {
                        PatientId = 1,
                        DoctorId = 1
                    }
                }
            };

            string notes = "Some notes";

            var record = new DiagnosisMedicalRecord()
            {
                Id = 1,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes,
                DiagnosedIllness = new Illness()
                {
                    Name = "Flu"
                },
                ProposedTreatment = new Treatment()
                {
                    PrescribedMedicine = "Panadol",
                    DurationInDays = 5
                }
            };

            var recordDto = new DiagnosisMedicalRecordFullInfoDto()
            {
                Id = 1,
                ExaminedPatient = new PatientShortInfoDto()
                {
                    FullName = patient.Name + patient.Surname
                },
                ResponsibleDoctor = new DoctorShortInfoDto()
                {
                    FullName = doctor.Name + doctor.Surname,
                    Department = "Heart diseases"
                },
                DateOfExamination = record.DateOfExamination,
                ExaminationNotes = notes,
                DiagnosedIllness = new IllnessRecordDto()
                {
                    Name = record.DiagnosedIllness.Name
                },
                ProposedTreatment = new TreatmentRecordDto()
                {
                    PrescribedMedicine = record.ProposedTreatment.PrescribedMedicine,
                    DurationInDays = record.ProposedTreatment.DurationInDays
                }
            };

            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(record.Id)).Returns(Task.FromResult(record));
            _mapperMock.Setup(mapper => mapper.Map<DiagnosisMedicalRecordFullInfoDto>(record))
                .Returns(new DiagnosisMedicalRecordFullInfoDto()
                {
                    Id = record.Id,
                    ExaminedPatient = new PatientShortInfoDto()
                    {
                        FullName = patient.Name + patient.Surname
                    },
                    ResponsibleDoctor = new DoctorShortInfoDto()
                    {
                        FullName = doctor.Name + doctor.Surname,
                        Department = "Heart diseases"
                    },
                    DateOfExamination = record.DateOfExamination,
                    ExaminationNotes = notes,
                    DiagnosedIllness = new IllnessRecordDto()
                    {
                        Name = record.DiagnosedIllness.Name
                    },
                    ProposedTreatment = new TreatmentRecordDto()
                    {
                        PrescribedMedicine = record.ProposedTreatment.PrescribedMedicine,
                        DurationInDays = record.ProposedTreatment.DurationInDays
                    }
                });

            var command = new GetDiagnosisMedicalRecordById(record.Id);
            var handler = new GetDiagnosisMedicalRecordByIdHandler(_recordRepositoryMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(recordDto.Id, result.Id);
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(record.Id), Times.Once);
        }

        [Fact]
        public async Task GetDiagnosisMedicalRecordById_ShouldThrowException_RecordDoesNotExist()
        {
            int recordId = 2;
            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId));

            var command = new GetDiagnosisMedicalRecordById(recordId);
            var handler = new GetDiagnosisMedicalRecordByIdHandler(_recordRepositoryMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
        }
    }
}
