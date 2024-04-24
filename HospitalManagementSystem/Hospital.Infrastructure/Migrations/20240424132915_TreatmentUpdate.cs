using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TreatmentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingHours_Doctors_DoctorScheduleId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorWorkingHours_Weekdays_WeekDayId",
                table: "DoctorWorkingHours");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_DiagnosisRecords_TreatmentId",
                table: "Treatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorWorkingHours",
                table: "DoctorWorkingHours");

            migrationBuilder.RenameTable(
                name: "DoctorWorkingHours",
                newName: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "IllnessSeverity",
                table: "Illnesses",
                newName: "Severity");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorWorkingHours_WeekDayId",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_WeekDayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules",
                column: "DoctorScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorScheduleId",
                table: "DoctorSchedules",
                column: "DoctorScheduleId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Weekdays_WeekDayId",
                table: "DoctorSchedules",
                column: "WeekDayId",
                principalTable: "Weekdays",
                principalColumn: "WeekDayId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_DiagnosisRecords_TreatmentId",
                table: "Treatments",
                column: "TreatmentId",
                principalTable: "DiagnosisRecords",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorScheduleId",
                table: "DoctorSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Weekdays_WeekDayId",
                table: "DoctorSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_DiagnosisRecords_TreatmentId",
                table: "Treatments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSchedules",
                table: "DoctorSchedules");

            migrationBuilder.RenameTable(
                name: "DoctorSchedules",
                newName: "DoctorWorkingHours");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "Illnesses",
                newName: "IllnessSeverity");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_WeekDayId",
                table: "DoctorWorkingHours",
                newName: "IX_DoctorWorkingHours_WeekDayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorWorkingHours",
                table: "DoctorWorkingHours",
                column: "DoctorScheduleId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_DiagnosisRecords_TreatmentId",
                table: "Treatments",
                column: "TreatmentId",
                principalTable: "DiagnosisRecords",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
