using Hospital.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hospital.Infrastructure
{
    public class HospitalManagementDbContext : DbContext
    {
        private string _dbConnectionString = "Server=ARTIFICIALBEAUT\\SQL_AMDARIS;Database=Hospital;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Patient> Patients { get; set; } = default!;
        public DbSet<Doctor> Doctors { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<DoctorWorkingHours> DoctorWorkingHours { get; set; } = default!;
        public DbSet<RegularMedicalRecord> RegularRecords { get; set; } = default!;
        public DbSet<DiagnosisMedicalRecord> DiagnosisRecords { get; set; } = default!;
        public DbSet<Illness> Illnesses { get; set; } = default!;
        public DbSet<Treatment> Treatments { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionString)
                          .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // department-doctor one-to-many
            modelBuilder.Entity<Doctor>()
                        .HasOne(d => d.Department)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            // doctor-patient many-to-many
            modelBuilder.Entity<Doctor>()
                        .HasMany(d => d.AssignedPatient)
                        .WithMany(p => p.AssignedDoctor);

            modelBuilder.Entity<Patient>()
                        .HasMany(p => p.AssignedDoctor)
                        .WithMany(d => d.AssignedPatient);

            // doctor-workinghours one-to-one
            modelBuilder.Entity<Doctor>()
                        .HasOne(d => d.WorkingHours)
                        .WithOne(wh => wh.Doctor)
                        .HasForeignKey<Doctor>(d => d.WorkingHoursId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            modelBuilder.Entity<DoctorWorkingHours>()
                        .HasOne(wh => wh.Doctor)
                        .WithOne(d => d.WorkingHours)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            // regularmedicalrecord-doctor many-to-one
            modelBuilder.Entity<RegularMedicalRecord>()
                        .HasOne(r => r.ResponsibleDoctor)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            // regularmedicalrecord-patient many-to-one
            modelBuilder.Entity<RegularMedicalRecord>()
                        .HasOne(r => r.ExaminedPatient)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            // diagnosismedicalrecord-doctor many-to-one
            modelBuilder.Entity<DiagnosisMedicalRecord>()
                        .HasOne(r => r.ResponsibleDoctor)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            // diagnosismedicalrecord-patient many-to-one
            modelBuilder.Entity<DiagnosisMedicalRecord>()
                        .HasOne(r => r.ExaminedPatient)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            // diagnosismedicalrecord-treatment one-to-one
            modelBuilder.Entity<DiagnosisMedicalRecord>()
                        .HasOne(r => r.ProposedTreatment)
                        .WithOne()
                        .HasForeignKey<DiagnosisMedicalRecord>(r => r.ProposedTreatmentId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            // diagnosismedicalrecord-illness one-to-one
            modelBuilder.Entity<DiagnosisMedicalRecord>()
                        .HasOne(r => r.DiagnosedIllness)
                        .WithMany()
                        .HasForeignKey(r => r.DiagnosedIllnessId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            //workinghours-weekday
            modelBuilder.Entity<DoctorWorkingHours>()
                        .HasOne(wh => wh.WeekDay)
                        .WithMany()
                        .HasForeignKey(wh => wh.WeekDayId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
        }
    }
}
