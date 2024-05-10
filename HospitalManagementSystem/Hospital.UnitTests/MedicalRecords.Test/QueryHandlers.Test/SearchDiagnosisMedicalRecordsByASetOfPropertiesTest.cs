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
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Hospital.Application.Illnesses.Responses;
using Hospital.Application.Treatments.Responses;

namespace Hospital.UnitTests.MedicalRecords.Test.QueryHandlers.Test
{
    public class SearchDiagnosisMedicalRecordsByASetOfPropertiesTest
    {
        private Mock<IRepository<DiagnosisMedicalRecord>> _recordRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public SearchDiagnosisMedicalRecordsByASetOfPropertiesTest()
        {
            _recordRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task SearchRegularMedicalRecordsByASetOfProperties_ShouldReturnListOfRecordFullInfoDtos()
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

            var records = new List<DiagnosisMedicalRecord>()
            {
                new()
                {
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
                }
            };

            _recordRepositoryMock.Setup(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<DiagnosisMedicalRecord, bool>>>()))
                .Returns(Task.FromResult(records));

            _mapperMock.Setup(mapper => mapper.Map<List<DiagnosisMedicalRecordFullInfoDto>>(records))
                .Returns(new List<DiagnosisMedicalRecordFullInfoDto>()
                {
                    new()
                    {
                        Id = records[0].Id,
                        ExaminedPatient = new PatientShortInfoDto()
                        {
                            FullName = patient.Name + patient.Surname
                        },
                        ResponsibleDoctor = new DoctorShortInfoDto()
                        {
                            FullName = doctor.Name + doctor.Surname,
                            Department = "Heart diseases"
                        },
                        DateOfExamination = records[0].DateOfExamination,
                        ExaminationNotes = notes,
                        DiagnosedIllness = new IllnessRecordDto()
                        {
                            Name = records[0].DiagnosedIllness.Name
                        },
                        ProposedTreatment = new TreatmentRecordDto()
                        {
                            PrescribedMedicine = records[0].ProposedTreatment.PrescribedMedicine,
                            DurationInDays = records[0].ProposedTreatment.DurationInDays
                        }
                    }
                });

            var command = new SearchDiagnosisMedicalRecordsByASetOfProperties(patient.Id, doctor.Id, null, "", "Panadol");
            var handler = new SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(_recordRepositoryMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(records.Count, result.Count);
            Assert.Equal(records[0].Id, result[0].Id);
            _recordRepositoryMock.Verify(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<DiagnosisMedicalRecord, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task SearchDiagnosisMedicalRecordsByASetOfProperties_ShouldThrowException_RecordsWithSuchPropertiesDoNotExist()
        {
            var records = new List<DiagnosisMedicalRecord>();

            int patientId = 1;
            int doctorId = 2;
            DateTimeOffset? date = null;
            string illness = "";
            string medicine = "Panadol";

            _recordRepositoryMock.Setup(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<DiagnosisMedicalRecord, bool>>>()))
                .Returns(Task.FromResult(records));

            var command = new SearchDiagnosisMedicalRecordsByASetOfProperties(patientId, doctorId, date, illness, medicine);
            var handler = new SearchDiagnosisMedicalRecordsByASetOfPropertiesHandler(_recordRepositoryMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _recordRepositoryMock.Verify(repo => repo.SearchByPropertyAsync(It.IsAny<Expression<Func<DiagnosisMedicalRecord, bool>>>()), Times.Once);
        }
    }
}
