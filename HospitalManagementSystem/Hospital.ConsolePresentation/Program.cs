using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var diContainer = new ServiceCollection()
    .AddDbContext<HospitalManagementDbContext>(options => 
            options.UseSqlServer("Server=ARTIFICIALBEAUT\\SQL_AMDARIS;Database=Hospital;Trusted_Connection=True;TrustServerCertificate=True;"))
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterNewHospitalDepartment).Assembly))
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddScoped<IPatientRepository, PatientRepository>()
    .AddScoped<IDoctorRepository, DoctorRepository>()
    .AddScoped<IRegularMedicalRecordRepository, RegularMedicalRecordRepository>()
    .AddScoped<IDiagnosisMedicalRecordRepository, DiagnosisMedicalRecordRepository>()
    .AddScoped<IDepartmentRepository, DepartmentRepository>()
    .AddScoped<IIllnessRepository, IllnessRepository>()
    .AddScoped<ITreatmentRepository, TreatmentRepository>()
    .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();
