using Hospital.Domain.Models.Utility;
using Hospital.Domain.Models;
using Hospital.Infrastructure;

namespace Hospital.IntegrationTests.Setup
{
    public class DbContextSeeder
    {
        public static void SeedData(HospitalManagementDbContext context)
        {
            var department = new Department()
            {
                Id = 1,
                Name = "Cardiology"
            };

            context.Departments.Add(department);
            context.SaveChanges();

            var patients = new List<Patient>()
            {
                new()
                {
                    Id = 1,
                    Name = "Mike",
                    Surname = "Douglas",
                    Age = 25,
                    Gender = Gender.M,
                    Address = "Chisinau, Moldova",
                    DoctorsPatients = new List<DoctorsPatients>()
                    {
                        new()
                        {
                            PatientId = 1,
                            DoctorId = 2
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Name = "Steven",
                    Surname = "Smith",
                    Age = 20,
                    Gender = Gender.M,
                    Address = "Balti, Moldova",
                    DoctorsPatients = new List<DoctorsPatients>()
                    {
                        new()
                        {
                            PatientId = 2,
                            DoctorId = 1
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    Name = "Agatha",
                    Surname = "Cruse",
                    Age = 52,
                    Gender = Gender.F,
                    Address = "Tiraspol, Moldova",
                    DoctorsPatients = new List<DoctorsPatients>()
                    {
                        new()
                        {
                            PatientId = 3,
                            DoctorId = 1
                        }
                    }
                }
            };

            context.Patients.AddRange(patients);
            context.SaveChanges();

            var doctors = new List<Doctor>()
            {
                new()
                {
                    Id = 1,
                    Name = "Greg",
                    Surname = "House",
                    Address = "Tiraspol, Moldova",
                    PhoneNumber = "069874589",
                    Department = department,
                    WorkingHours = new DoctorSchedule()
                    {
                        StartShift = new TimeSpan(10, 0, 0),
                        EndShift = new TimeSpan(18, 0, 0)
                    },
                },
                new()
                {
                    Id = 2,
                    Name = "Bill",
                    Surname = "Murray",
                    Address = "Edinet, Moldova",
                    PhoneNumber = "074563123",
                    Department = department,
                    WorkingHours = new DoctorSchedule()
                    {
                        StartShift = new TimeSpan(8, 0, 0),
                        EndShift = new TimeSpan(21, 0, 0)
                    },
                }
            };

            context.Doctors.AddRange(doctors);
            context.SaveChanges();

            var regularRecords = new List<RegularMedicalRecord>()
            {
                new()
                {
                    ExaminedPatient = patients[0],
                    ResponsibleDoctor = doctors[1],
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = "Some notes",
                },
                new()
                {
                    ExaminedPatient = patients[1],
                    ResponsibleDoctor = doctors[0],
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = "Another notes",
                },
                new()
                {
                    ExaminedPatient = patients[2],
                    ResponsibleDoctor = doctors[0],
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = "Some more notes",
                }
            };

            context.RegularRecords.AddRange(regularRecords);
            context.SaveChanges();

            var diagnosisRecords = new List<DiagnosisMedicalRecord>()
            {
                new()
                {
                    ExaminedPatient = patients[0],
                    ResponsibleDoctor = doctors[1],
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = "Some notes",
                    DiagnosedIllness = new Illness()
                    {
                        Name = "Arrhytmia"
                    },
                    ProposedTreatment = new Treatment()
                    {
                        PrescribedMedicine = "Corvalment",
                        DurationInDays = 7
                    }
                },
                new()
                {
                    ExaminedPatient = patients[1],
                    ResponsibleDoctor = doctors[0],
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = "Another notes",
                    DiagnosedIllness = new Illness()
                    {
                        Name = "Hypertensia"
                    },
                    ProposedTreatment = new Treatment()
                    {
                        PrescribedMedicine = "Valocordin",
                        DurationInDays = 14
                    }
                },
                new()
                {
                    ExaminedPatient = patients[2],
                    ResponsibleDoctor = doctors[0],
                    DateOfExamination = DateTimeOffset.UtcNow,
                    ExaminationNotes = "Some more notes",
                    DiagnosedIllness = new Illness()
                    {
                        Name = "Hypotensia"
                    },
                    ProposedTreatment = new Treatment()
                    {
                        PrescribedMedicine = "Medical tea",
                        DurationInDays = 7
                    }
                }
            };

            context.DiagnosisRecords.AddRange(diagnosisRecords);
            context.SaveChanges();
        }
    }
}
