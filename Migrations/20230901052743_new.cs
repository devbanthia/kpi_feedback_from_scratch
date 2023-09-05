using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace kpi_feedback_from_scratch.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

               
            migrationBuilder.CreateTable(
                name: "kpi_assessor_feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    KPI_AssessorId = table.Column<int>(type: "int", nullable: false),
                    rating_type_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<string>(type: "longtext", nullable: true),
                    AreaOfStrength = table.Column<string>(type: "longtext", nullable: true),
                    Improvement = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kpi_assessor_feedback", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            

      
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable("kpi_assessor_feedback");
        }
    }
}
