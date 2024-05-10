﻿using Hospital.Application.Abstractions;
using Hospital.Application.Exceptions;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Moq;

namespace Hospital.UnitTests.MedicalRecords.Test.CommandHandlers.Test
{
    public class RemoveWronglyAddedDiagnosisMedicalRecordTest
    {
        public class RemoveWronglyAddedRegularMedicalRecordTest
        {
            private Mock<IRepository<DiagnosisMedicalRecord>> _recordRepositoryMock;

            public RemoveWronglyAddedRegularMedicalRecordTest()
            {
                _recordRepositoryMock = new();
            }

            [Fact]
            public async Task RemoveWronglyAddedDiagnosisMedicalRecord_ShouldReturnRemovedRecordId()
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

                _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(record.Id)).Returns(Task.FromResult(record));

                var command = new RemoveWronglyAddedDiagnosisMedicalRecord(doctor.Id);
                var handler = new RemoveWronglyAddedDiagnosisMedicalRecordHandler(_recordRepositoryMock.Object);

                var result = await handler.Handle(command, default);

                Assert.Equal(record.Id, result);
                _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(doctor.Id), Times.Once);
                _recordRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<DiagnosisMedicalRecord>()), Times.Once); ;
            }

            [Fact]
            public async Task RemoveWronglyAddedDiagnosisMedicalRecord_ShouldThrowException_RecordToRemoveDoesNotExist()
            {
                int recordId = 2;
                _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(recordId));

                var command = new RemoveWronglyAddedDiagnosisMedicalRecord(recordId);
                var handler = new RemoveWronglyAddedDiagnosisMedicalRecordHandler(_recordRepositoryMock.Object);

                await Assert.ThrowsAsync<NoEntityFoundException>(() => handler.Handle(command, default));
                _recordRepositoryMock.Verify(repo => repo.GetByIdAsync(recordId), Times.Once);
            }
        }
    }
}
