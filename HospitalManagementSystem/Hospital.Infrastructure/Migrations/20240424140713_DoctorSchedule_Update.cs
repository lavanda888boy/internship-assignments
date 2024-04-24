using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoctorSchedule_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Weekdays_WeekDayId",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_WeekDayId",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "WeekDayId",
                table: "DoctorSchedules");

            migrationBuilder.CreateTable(
                name: "DoctorScheduleWeekDay",
                columns: table => new
                {
                    DoctorScheduleId = table.Column<int>(type: "int", nullable: false),
                    WeekDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorScheduleWeekDay", x => new { x.DoctorScheduleId, x.WeekDayId });
                    table.ForeignKey(
                        name: "FK_DoctorScheduleWeekDay_DoctorSchedules_DoctorScheduleId",
                        column: x => x.DoctorScheduleId,
                        principalTable: "DoctorSchedules",
                        principalColumn: "DoctorScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorScheduleWeekDay_Weekdays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "Weekdays",
                        principalColumn: "WeekDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleWeekDay_WeekDayId",
                table: "DoctorScheduleWeekDay",
                column: "WeekDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorScheduleWeekDay");

            migrationBuilder.AddColumn<int>(
                name: "WeekDayId",
                table: "DoctorSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_WeekDayId",
                table: "DoctorSchedules",
                column: "WeekDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Weekdays_WeekDayId",
                table: "DoctorSchedules",
                column: "WeekDayId",
                principalTable: "Weekdays",
                principalColumn: "WeekDayId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
