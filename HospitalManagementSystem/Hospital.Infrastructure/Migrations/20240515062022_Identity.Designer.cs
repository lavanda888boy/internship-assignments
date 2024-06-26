﻿// <auto-generated />
using System;
using Hospital.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    [DbContext(typeof(HospitalManagementDbContext))]
    [Migration("20240515062022_Identity")]
    partial class Identity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hospital.Domain.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DepartmentId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Hospital.Domain.Models.DiagnosisMedicalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RecordId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("DateOfExamination")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("DiagnosedIllnessId")
                        .HasColumnType("int");

                    b.Property<string>("ExaminationNotes")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ExaminedPatientId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleDoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiagnosedIllnessId");

                    b.HasIndex("ExaminedPatientId");

                    b.HasIndex("ResponsibleDoctorId");

                    b.ToTable("DiagnosisRecords");
                });

            modelBuilder.Entity("Hospital.Domain.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DoctorId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Hospital.Domain.Models.DoctorSchedule", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("DoctorScheduleId");

                    b.Property<TimeSpan>("EndShift")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartShift")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("DoctorSchedules");
                });

            modelBuilder.Entity("Hospital.Domain.Models.DoctorScheduleWeekDay", b =>
                {
                    b.Property<int>("DoctorScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("WeekDayId")
                        .HasColumnType("int");

                    b.HasKey("DoctorScheduleId", "WeekDayId");

                    b.HasIndex("WeekDayId");

                    b.ToTable("DoctorScheduleWeekDay");
                });

            modelBuilder.Entity("Hospital.Domain.Models.DoctorsPatients", b =>
                {
                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("DoctorId", "PatientId");

                    b.HasIndex("PatientId");

                    b.ToTable("DoctorsPatients");
                });

            modelBuilder.Entity("Hospital.Domain.Models.Illness", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("IllnessId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Illnesses");
                });

            modelBuilder.Entity("Hospital.Domain.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PatientId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("InsuranceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Hospital.Domain.Models.RegularMedicalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RecordId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("DateOfExamination")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ExaminationNotes")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ExaminedPatientId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleDoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExaminedPatientId");

                    b.HasIndex("ResponsibleDoctorId");

                    b.ToTable("RegularRecords");
                });

            modelBuilder.Entity("Hospital.Domain.Models.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("TreatmentId");

                    b.Property<int>("DurationInDays")
                        .HasColumnType("int");

                    b.Property<string>("PrescribedMedicine")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("Hospital.Domain.Models.WeekDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("WeekDayId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Weekdays");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Hospital.Domain.Models.DiagnosisMedicalRecord", b =>
                {
                    b.HasOne("Hospital.Domain.Models.Illness", "DiagnosedIllness")
                        .WithMany()
                        .HasForeignKey("DiagnosedIllnessId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Hospital.Domain.Models.Patient", "ExaminedPatient")
                        .WithMany()
                        .HasForeignKey("ExaminedPatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Hospital.Domain.Models.Doctor", "ResponsibleDoctor")
                        .WithMany()
                        .HasForeignKey("ResponsibleDoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DiagnosedIllness");

                    b.Navigation("ExaminedPatient");

                    b.Navigation("ResponsibleDoctor");
                });

            modelBuilder.Entity("Hospital.Domain.Models.Doctor", b =>
                {
                    b.HasOne("Hospital.Domain.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Hospital.Domain.Models.DoctorSchedule", b =>
                {
                    b.HasOne("Hospital.Domain.Models.Doctor", null)
                        .WithOne("WorkingHours")
                        .HasForeignKey("Hospital.Domain.Models.DoctorSchedule", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hospital.Domain.Models.DoctorScheduleWeekDay", b =>
                {
                    b.HasOne("Hospital.Domain.Models.DoctorSchedule", "DoctorSchedule")
                        .WithMany("DoctorScheduleWeekDay")
                        .HasForeignKey("DoctorScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital.Domain.Models.WeekDay", "WeekDay")
                        .WithMany("DoctorScheduleWeekDay")
                        .HasForeignKey("WeekDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DoctorSchedule");

                    b.Navigation("WeekDay");
                });

            modelBuilder.Entity("Hospital.Domain.Models.DoctorsPatients", b =>
                {
                    b.HasOne("Hospital.Domain.Models.Doctor", "Doctor")
                        .WithMany("DoctorsPatients")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital.Domain.Models.Patient", "Patient")
                        .WithMany("DoctorsPatients")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Hospital.Domain.Models.RegularMedicalRecord", b =>
                {
                    b.HasOne("Hospital.Domain.Models.Patient", "ExaminedPatient")
                        .WithMany()
                        .HasForeignKey("ExaminedPatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Hospital.Domain.Models.Doctor", "ResponsibleDoctor")
                        .WithMany()
                        .HasForeignKey("ResponsibleDoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ExaminedPatient");

                    b.Navigation("ResponsibleDoctor");
                });

            modelBuilder.Entity("Hospital.Domain.Models.Treatment", b =>
                {
                    b.HasOne("Hospital.Domain.Models.DiagnosisMedicalRecord", null)
                        .WithOne("ProposedTreatment")
                        .HasForeignKey("Hospital.Domain.Models.Treatment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hospital.Domain.Models.DiagnosisMedicalRecord", b =>
                {
                    b.Navigation("ProposedTreatment")
                        .IsRequired();
                });

            modelBuilder.Entity("Hospital.Domain.Models.Doctor", b =>
                {
                    b.Navigation("DoctorsPatients");

                    b.Navigation("WorkingHours")
                        .IsRequired();
                });

            modelBuilder.Entity("Hospital.Domain.Models.DoctorSchedule", b =>
                {
                    b.Navigation("DoctorScheduleWeekDay");
                });

            modelBuilder.Entity("Hospital.Domain.Models.Patient", b =>
                {
                    b.Navigation("DoctorsPatients");
                });

            modelBuilder.Entity("Hospital.Domain.Models.WeekDay", b =>
                {
                    b.Navigation("DoctorScheduleWeekDay");
                });
#pragma warning restore 612, 618
        }
    }
}
