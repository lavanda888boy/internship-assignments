using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoctorPatient_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingHours_Doctors_DoctorWorkingHoursId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingHours_WeekDay_WeekDayId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropTable(
                name: "DoctorPatient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeekDay",
                table: "WeekDay");

            migrationBuilder.RenameTable(
                name: "WeekDay",
                newName: "Weekdays");

            migrationBuilder.RenameColumn(
                name: "TreatmentDuration",
                table: "Treatments",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "DoctorWorkingHoursId",
                table: "DoctorWorkingHours",
                newName: "DoctorScheduleId");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Doctors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weekdays",
                table: "Weekdays",
                column: "WeekDayId");

            migrationBuilder.CreateTable(
                name: "DoctorsPatients",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsPatients", x => new { x.DoctorId, x.PatientId });
                    table.ForeignKey(
                        name: "FK_DoctorsPatients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorsPatients_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsPatients_PatientId",
                table: "DoctorsPatients",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingHours_Doctors_DoctorScheduleId",
                table: "DoctorWorkingHours",
                column: "DoctorScheduleId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingHours_Weekdays_WeekDayId",
                table: "DoctorWorkingHours",
                column: "WeekDayId",
                principalTable: "Weekdays",
                principalColumn: "WeekDayId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingHours_Doctors_DoctorScheduleId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingHours_Weekdays_WeekDayId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropTable(
                name: "DoctorsPatients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weekdays",
                table: "Weekdays");

            migrationBuilder.RenameTable(
                name: "Weekdays",
                newName: "WeekDay");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Treatments",
                newName: "TreatmentDuration");

            migrationBuilder.RenameColumn(
                name: "DoctorScheduleId",
                table: "DoctorWorkingHours",
                newName: "DoctorWorkingHoursId");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Patients",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Doctors",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Doctors",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeekDay",
                table: "WeekDay",
                column: "WeekDayId");

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

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatient_AssignedPatientId",
                table: "DoctorPatient",
                column: "AssignedPatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingHours_Doctors_DoctorWorkingHoursId",
                table: "DoctorWorkingHours",
                column: "DoctorWorkingHoursId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingHours_WeekDay_WeekDayId",
                table: "DoctorWorkingHours",
                column: "WeekDayId",
                principalTable: "WeekDay",
                principalColumn: "WeekDayId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
