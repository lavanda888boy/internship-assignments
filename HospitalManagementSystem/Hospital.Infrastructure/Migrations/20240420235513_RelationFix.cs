using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Doctors_AssignedDoctorsId",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Patients_AssignedPatientsId",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_WeekDay_DoctorWorkingHours_DoctorWorkingHoursId",
                table: "WeekDay");

            migrationBuilder.DropIndex(
                name: "IX_WeekDay_DoctorWorkingHoursId",
                table: "WeekDay");

            migrationBuilder.DropColumn(
                name: "DoctorWorkingHoursId",
                table: "WeekDay");

            migrationBuilder.RenameColumn(
                name: "AssignedPatientsId",
                table: "DoctorPatient",
                newName: "AssignedPatientId");

            migrationBuilder.RenameColumn(
                name: "AssignedDoctorsId",
                table: "DoctorPatient",
                newName: "AssignedDoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorPatient_AssignedPatientsId",
                table: "DoctorPatient",
                newName: "IX_DoctorPatient_AssignedPatientId");

            migrationBuilder.AddColumn<int>(
                name: "WeekDayId",
                table: "DoctorWorkingHours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorWorkingHours_WeekDayId",
                table: "DoctorWorkingHours",
                column: "WeekDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Doctors_AssignedDoctorId",
                table: "DoctorPatient",
                column: "AssignedDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Patients_AssignedPatientId",
                table: "DoctorPatient",
                column: "AssignedPatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorWorkingHours_WeekDay_WeekDayId",
                table: "DoctorWorkingHours",
                column: "WeekDayId",
                principalTable: "WeekDay",
                principalColumn: "WeekDayId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Doctors_AssignedDoctorId",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Patients_AssignedPatientId",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingHours_WeekDay_WeekDayId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropIndex(
                name: "IX_DoctorWorkingHours_WeekDayId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropColumn(
                name: "WeekDayId",
                table: "DoctorWorkingHours");

            migrationBuilder.RenameColumn(
                name: "AssignedPatientId",
                table: "DoctorPatient",
                newName: "AssignedPatientsId");

            migrationBuilder.RenameColumn(
                name: "AssignedDoctorId",
                table: "DoctorPatient",
                newName: "AssignedDoctorsId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorPatient_AssignedPatientId",
                table: "DoctorPatient",
                newName: "IX_DoctorPatient_AssignedPatientsId");

            migrationBuilder.AddColumn<int>(
                name: "DoctorWorkingHoursId",
                table: "WeekDay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WeekDay_DoctorWorkingHoursId",
                table: "WeekDay",
                column: "DoctorWorkingHoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Doctors_AssignedDoctorsId",
                table: "DoctorPatient",
                column: "AssignedDoctorsId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Patients_AssignedPatientsId",
                table: "DoctorPatient",
                column: "AssignedPatientsId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeekDay_DoctorWorkingHours_DoctorWorkingHoursId",
                table: "WeekDay",
                column: "DoctorWorkingHoursId",
                principalTable: "DoctorWorkingHours",
                principalColumn: "DoctorWorkingHoursId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
