using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationFix_20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiagnosisRecords_DiagnosedIllnessId",
                table: "DiagnosisRecords");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisRecords_DiagnosedIllnessId",
                table: "DiagnosisRecords",
                column: "DiagnosedIllnessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiagnosisRecords_DiagnosedIllnessId",
                table: "DiagnosisRecords");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisRecords_DiagnosedIllnessId",
                table: "DiagnosisRecords",
                column: "DiagnosedIllnessId",
                unique: true);
        }
    }
}
