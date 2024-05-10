using AutoMapper;
using Hospital.Application.Abstractions;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Application.MedicalRecords.Responses;
using Hospital.Application.Patients.Responses;
using Hospital.Domain.Models;
using Hospital.Domain.Models.Utility;
using Moq;

namespace Hospital.UnitTests.MedicalRecords.Test.QueryHandlers.Test
{
    public class GetRegularMedicalRecordByIdTest
    {
        private Mock<IRepository<RegularMedicalRecord>> _recordRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public GetRegularMedicalRecordByIdTest()
        {
            _recordRepositoryMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task GetRegularMedicalRecordById_ShouldReturnMedicalRecordFullInfoDto()
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

            var record = new RegularMedicalRecord()
            {
                Id = 1,
                ExaminedPatient = patient,
                ResponsibleDoctor = doctor,
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes
            };

            var recordDto = new RegularMedicalRecordFullInfoDto()
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
                DateOfExamination = DateTimeOffset.Now,
                ExaminationNotes = notes
            };

            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(record.Id)).Returns(Task.FromResult(record));
            _mapperMock.Setup(mapper => mapper.Map<RegularMedicalRecordFullInfoDto>(record))
                .Returns(new RegularMedicalRecordFullInfoDto()
                {
                    Id = recordDto.Id,
                    ExaminedPatient = new PatientShortInfoDto()
                    {
                        FullName = patient.Name + patient.Surname
                    },
                    ResponsibleDoctor = new DoctorShortInfoDto()
                    {
                        FullName = doctor.Name + doctor.Surname,
                        Department = "Heart diseases"
                    },
                    DateOfExamination = recordDto.DateOfExamination,
                    ExaminationNotes = notes
                });

            var command = new GetRegularMedicalRecordById(record.Id);
            var handler = new GetRegularMedicalRecordByIdHandler(_recordRepositoryMock.Object, _mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.Equal(recordDto.Id, result.Id);
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(record.Id), Times.Once);
        }

        [Fact]
        public async Task GetRegularMedicalRecordById_ShouldThrowException_RecordDoesNotExist()
        {
            int recordId = 2;
            _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId));

            var command = new GetRegularMedicalRecordById(recordId);
            var handler = new GetRegularMedicalRecordByIdHandler(_recordRepositoryMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
            _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
        }
    }
}
