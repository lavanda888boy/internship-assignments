using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Domain.Models;
using Hospital.Infrastructure;
using Hospital.Infrastructure.AI;
using Hospital.Infrastructure.Repository;
using Hospital.Presentation.Extensions;
using Hospital.Presentation.Filters;
using Hospital.Presentation.Middleware;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
                options.Filters.Add<HospitalManagementExceptionHandler>()
            );
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<HospitalManagementDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<ModelValidationFilter>();

            builder.RegisterAuthentication();
            builder.Services.AddAuthToSwagger();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterNewHospitalDepartment).Assembly));
            builder.Services.AddScoped<IRepository<Patient>, PatientRepository>()
                            .AddScoped<IRepository<Doctor>, DoctorRepository>()
                            .AddScoped<IRepository<RegularMedicalRecord>, RegularMedicalRecordRepository>()
                            .AddScoped<IRepository<DiagnosisMedicalRecord>, DiagnosisMedicalRecordRepository>()
                            .AddScoped<IRepository<Department>, DepartmentRepository>()
                            .AddScoped<IRepository<Illness>, IllnessRepository>()
                            .AddScoped<IRepository<Treatment>, TreatmentRepository>();

            builder.Services.AddAutoMapper(typeof(ListAllPaginatedRegularMedicalRecords));

            builder.Services.AddScoped<IMedicalAdviceGenerationService, MedicalAdviceGenerationService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalHostReactApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5173")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<RequestTimingMiddleware>();

            app.UseMiddleware<DbTransactionMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors("AllowLocalHostReactApp");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
