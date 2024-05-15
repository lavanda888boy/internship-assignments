using Hospital.Application.Abstractions;
using Hospital.Application.Departments.Commands;
using Hospital.Application.MedicalRecords.Queries;
using Hospital.Domain.Models;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Repository;
using Hospital.Presentation;
using Hospital.Presentation.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.IntegrationTests.Setup
{
    public class HospitalWebAppFactory<T> : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HospitalManagementDbContext>));
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddSingleton(serviceProvider);

                services.AddDbContext<HospitalManagementDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryHospitalTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var appContext = scope.ServiceProvider.GetRequiredService<HospitalManagementDbContext>();
                try
                {
                    appContext.Database.EnsureCreated();
                    DbContextSeeder.SeedData(appContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        
    }
}

