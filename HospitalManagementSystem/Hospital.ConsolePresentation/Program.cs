using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
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