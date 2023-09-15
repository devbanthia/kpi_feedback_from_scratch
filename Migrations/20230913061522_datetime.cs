using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kpi_feedback_from_scratch.Migrations
{
    /// <inheritdoc />
    public partial class datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KPI_Assessment_Due_Date",
                table: "kpi_assessor");

            migrationBuilder.AddColumn<DateTime>(
                name: "Next_Feedback_Date",
                table: "kpi_assessor_feedback",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Next_Feedback_Date",
                table: "kpi_assessor_feedback");

            migrationBuilder.AddColumn<DateTime>(
                name: "KPI_Assessment_Due_Date",
                table: "kpi_assessor",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
