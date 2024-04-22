using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Illnesses",
                columns: table => new
                {
                    IllnessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IllnessSeverity = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Illnesses", x => x.IllnessId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "WeekDay",
                columns: table => new
                {
                    WeekDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDay", x => x.WeekDayId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    WorkingHoursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisRecords",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminedPatientId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleDoctorId = table.Column<int>(type: "int", nullable: false),
                    DateOfExamination = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExaminationNotes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DiagnosedIllnessId = table.Column<int>(type: "int", nullable: false),
                    ProposedTreatmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_DiagnosisRecords_Doctors_ResponsibleDoctorId",
                        column: x => x.ResponsibleDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosisRecords_Illnesses_DiagnosedIllnessId",
                        column: x => x.DiagnosedIllnessId,
                        principalTable: "Illnesses",
                        principalColumn: "IllnessId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiagnosisRecords_Patients_ExaminedPatientId",
                        column: x => x.ExaminedPatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorPatient",
                columns: table => new
                {
                    AssignedDoctorId = table.Column<int>(type: "int", nullable: false),
                    AssignedPatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatient", x => new { x.AssignedDoctorId, x.AssignedPatientId });
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Doctors_AssignedDoctorId",
                        column: x => x.AssignedDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatient_Patients_AssignedPatientId",
                        column: x => x.AssignedPatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorWorkingHours",
                columns: table => new
                {
                    DoctorWorkingHoursId = table.Column<int>(type: "int", nullable: false),
                    StartShift = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndShift = table.Column<TimeSpan>(type: "time", nullable: false),
                    WeekDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorWorkingHours", x => x.DoctorWorkingHoursId);
                    table.ForeignKey(
                        name: "FK_DoctorWorkingHours_Doctors_DoctorWorkingHoursId",
                        column: x => x.DoctorWorkingHoursId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoctorWorkingHours_WeekDay_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDay",
                        principalColumn: "WeekDayId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegularRecords",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExaminedPatientId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleDoctorId = table.Column<int>(type: "int", nullable: false),
                    DateOfExamination = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExaminationNotes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_RegularRecords_Doctors_ResponsibleDoctorId",
                        column: x => x.ResponsibleDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegularRecords_Patients_ExaminedPatientId",
                        column: x => x.ExaminedPatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    PrescribedMedicine = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TreatmentDuration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_Treatments_DiagnosisRecords_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "DiagnosisRecords",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisRecords_DiagnosedIllnessId",
                table: "DiagnosisRecords",
                column: "DiagnosedIllnessId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisRecords_ExaminedPatientId",
                table: "DiagnosisRecords",
                column: "ExaminedPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisRecords_ResponsibleDoctorId",
                table: "DiagnosisRecords",
                column: "ResponsibleDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_AssignedPatientId",
                table: "DoctorPatient",
                column: "AssignedPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorWorkingHours_WeekDayId",
                table: "DoctorWorkingHours",
                column: "WeekDayId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularRecords_ExaminedPatientId",
                table: "RegularRecords",
                column: "ExaminedPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularRecords_ResponsibleDoctorId",
                table: "RegularRecords",
                column: "ResponsibleDoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorPatient");

            migrationBuilder.DropTable(
                name: "DoctorWorkingHours");

            migrationBuilder.DropTable(
                name: "RegularRecords");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "WeekDay");

            migrationBuilder.DropTable(
                name: "DiagnosisRecords");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Illnesses");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
