using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Application.Doctors.Commands;
using Hospital.Application.Doctors.Queries;
using Hospital.Application.Doctors.Responses;
using Hospital.Application.Illnesses.Commands;
using Hospital.Application.MedicalRecords.Commands;
using Hospital.Application.Patients.Commands;
using Hospital.Application.Patients.Queries;
using Hospital.Application.Patients.Responses;
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

var department = await mediator.Send(new RegisterNewHospitalDepartment(1, "Heart diseases"));
Console.WriteLine(department.Name);

var illness = await mediator.Send(new RegisterExistingIllness(1, "flu", IllnessSeverity.MEDIUM));
Console.WriteLine(illness.Name);

var doctor1 = await mediator.Send(new EmployNewDoctor(1, "Seva", "Bajenov", "Dacia 24", "089425697",
    1, 1, new TimeSpan(7, 0, 0), new TimeSpan(19, 0, 0), new List<DayOfWeek>() { DayOfWeek.Monday }));
var doctor2 = await mediator.Send(new EmployNewDoctor(2, "Greg", "House", "Malibu", "085415794",
    1, 1, new TimeSpan(10, 0, 0), new TimeSpan(18, 0, 0), new List<DayOfWeek>() { DayOfWeek.Tuesday }));

var patient = await mediator.Send(new RegisterNewPatient(1, "Steve", "Black", 25, "M", "Palm beach", null, "AB2525"));
Console.WriteLine(patient.AssignedDoctors.Count);

var updatedPatient = await mediator.Send(new UpdatePatientAssignedDoctors(1, new List<int> { 2 }));
var assignedDoctorNames = updatedPatient.AssignedDoctors.Select(d => d.Name).ToList();
Console.WriteLine(assignedDoctorNames[0]);

var updatedDoctor = await mediator.Send(new UpdateDoctorAssignedPatients(1, new List<int> { }));
Console.WriteLine(updatedDoctor.AssignedPatients.Count);

var record = await mediator.Send(new AddNewDiagnosisMedicalRecord(1, 1, 2, "patient is ill", 1, 1,
    "panadol", new TimeSpan(7, 0, 0, 0)));
Console.WriteLine(record.ProposedTreatment.PrescribedMedicine);

var updatedRecord = await mediator.Send(new AdjustDiagnosisMedicalRecordExaminationNotes(1, "Patient is not so ill"));
Console.WriteLine(updatedRecord.ExaminationNotes);

PatientFilterDto pFilters = new PatientFilterDto()
{
    Name = "John",
};
var filteredPatients = await mediator.Send(new SearchPatientsByASetOfProperties(pFilters));
Console.WriteLine(filteredPatients.Count);

DoctorFilterDto dFilters = new DoctorFilterDto()
{
    Name = "Seva",
    Address = "Malibu",
};
var filteredDoctors = await mediator.Send(new SearchDoctorsByASetOfProperties(dFilters));
Console.WriteLine(filteredDoctors.Count);