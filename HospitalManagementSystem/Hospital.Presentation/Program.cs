using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Domain.Models;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Repository;
using Hospital.Presentation.Filters;
using Hospital.Presentation.Middleware;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
                options.Filters.Add<HospitalManagementExceptionHandler>()
            );
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HospitalManagementDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterNewHospitalDepartment).Assembly));
            builder.Services.AddScoped<IRepository<Patient>, PatientRepository>()
                            .AddScoped<IRepository<Doctor>, DoctorRepository>()
                            .AddScoped<IRepository<RegularMedicalRecord>, RegularMedicalRecordRepository>()
                            .AddScoped<IRepository<DiagnosisMedicalRecord>, DiagnosisMedicalRecordRepository>()
                            .AddScoped<IRepository<Department>, DepartmentRepository>()
                            .AddScoped<IRepository<Illness>, IllnessRepository>()
                            .AddScoped<IRepository<Treatment>, TreatmentRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<RequestTimingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
