using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Domain.Models;
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
    .AddScoped<IRepository<Patient>, PatientRepository>()
    .AddScoped<IRepository<Doctor>, DoctorRepository>()
    .AddScoped<IRepository<RegularMedicalRecord>, RegularMedicalRecordRepository>()
    .AddScoped<IRepository<DiagnosisMedicalRecord>, DiagnosisMedicalRecordRepository>()
    .AddScoped<IRepository<Department>, DepartmentRepository>()
    .AddScoped<IRepository<Illness>, IllnessRepository>()
    .AddScoped<IRepository<Treatment>, TreatmentRepository>()
    .BuildServiceProvider();

var mediator = diContainer.GetRequiredService<IMediator>();
