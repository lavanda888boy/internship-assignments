using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Application.Doctors.Commands;
using Hospital.Application.Doctors.Queries;
using Hospital.Application.Illnesses.Commands;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.Patients.Commands;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Treatments.Queries;
using Hospital.Domain.Models.Utility;
using Hospital.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var diContainer = new ServiceCollection()
    .AddSingleton<IPatientRepository, PatientRepository>()
    .AddSingleton<IDoctorRepository, DoctorRepository>()
    .AddSingleton<IRegularMedicalRecordRepository, RegularMedicalRecordRepository>()
    .AddSingleton<IDiagnosisMedicalRecordRepository, DiagnosisMedicalRecordRepository>()
    .AddSingleton<IDepartmentRepository, DepartmentRepository>()
    .AddSingleton<IIllnessRepository, IllnessRepository>()
    .AddSingleton<ITreatmentRepository, TreatmentRepository>()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IPatientRepository).Assembly))
    .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();

var department = await mediator.Send(new CreateDepartment(1, "Heart diseases"));
Console.WriteLine(department.Name);

var illness = await mediator.Send(new CreateIllness(1, "flu", IllnessSeverity.MEDIUM));
Console.WriteLine(illness.Name);

var doctor1 = await mediator.Send(new CreateDoctor(1, "Seva", "Bajenov", "Dacia 24", "089425697",
    1, 1, new TimeSpan(7, 0, 0), new TimeSpan(19, 0, 0)));
var doctor2 = await mediator.Send(new CreateDoctor(2, "Greg", "House", "Malibu", "085415794",
    1, 1, new TimeSpan(10, 0, 0), new TimeSpan(18, 0, 0)));

var patient = await mediator.Send(new CreatePatient(1, "Steve", "Black", 25, "M", "Palm beach", null, "AB2525"));
//var assignedDoctorNames = patient.AssignedDoctors.Select(d => d.Name).ToList();
//var doctor = await mediator.Send(new GetDoctorById(1));
//Console.WriteLine(doctor.AssignedPatients.Count);

/*var updatedPatient = await mediator.Send(new UpdatePatient(1, "Steve", "Black", 25, "M", "Palm beach", null, "AB2525", new List<int> { 2 }));
var assignedDoctorNames = updatedPatient.AssignedDoctors.Select(d => d.Name).ToList();
Console.WriteLine(assignedDoctorNames[0]);
var doctor = await mediator.Send(new GetDoctorById(1));
Console.WriteLine(doctor.AssignedPatients.Count);*/

/*var updatedDoctor = await mediator.Send(new UpdateDoctor(1, "Seva", "Bajenov", "Dacia 24", "089425697",
    1, new List<int> { }, 1, new TimeSpan(7, 0, 0), new TimeSpan(19, 0, 0)));
var p = await mediator.Send(new GetPatientById(1));
Console.WriteLine(p.AssignedDoctors.Count);*/

var record = await mediator.Send(new CreateDiagnosisMedicalRecord(1, 1, 2, DateTimeOffset.UtcNow, 
    "patient is ill", 1, 1, "panadol", new TimeSpan(7, 0, 0, 0)));
var treatment = await mediator.Send(new GetTreatmentById(1));
Console.WriteLine(treatment.PrescribedMedicine);