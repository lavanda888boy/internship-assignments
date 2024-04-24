using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Doctor_DoctorSchedule_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorScheduleId",
                table: "DoctorSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorScheduleId",
                table: "DoctorSchedules",
                column: "DoctorScheduleId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorScheduleId",
                table: "DoctorSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorScheduleId",
                table: "DoctorSchedules",
                column: "DoctorScheduleId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
