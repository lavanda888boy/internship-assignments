using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.MedicalRecords.Test.QueryHandlers.Test
{
    public class ListAllRegularMedicalRecordsTest
    {
        private Mock<IRepository<RegularMedicalRecord>> _recordRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public ListAllRegularMedicalRecordsTest()
        {
            _recordRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task ListAllRegularMedicalRecords_ShouldReturnListOfMedicalRecordDtos()
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

            var records = new List<RegularMedicalRecord>()
            {
                new()
                {
                    ExaminedPatient = patient,
                    ResponsibleDoctor = doctor,
                    DateOfExamination = DateTimeOffset.Now,
                    ExaminationNotes = notes
                }
            };

            _recordRepositoryMock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(records));
            _mapperMock.Setup(mapper => mapper.Map<List<RegularMedicalRecordFullInfoDto>>(records))
                .Returns(new List<RegularMedicalRecordFullInfoDto>()
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
                        ExaminationNotes = notes
                    }
                });

            var command = new ListAllRegularMedicalRecords();
            var handler = new ListAllRegularMedicalRecordsHandler(_recordRepositoryMock.Object, _mapperMock.Object);
            
            var result = await handler.Handle(command, default);

            Assert.Equal(records.Count, result.Count);
            Assert.Equal(records[0].Id, result[0].Id);
            _recordRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}
