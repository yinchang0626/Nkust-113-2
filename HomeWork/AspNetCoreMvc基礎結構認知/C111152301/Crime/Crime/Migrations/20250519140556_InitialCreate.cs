using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crime.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrimeStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCases = table.Column<int>(type: "INTEGER", nullable: false),
                    RapeCases = table.Column<int>(type: "INTEGER", nullable: false),
                    RobberyCases = table.Column<int>(type: "INTEGER", nullable: false),
                    OtherCases = table.Column<int>(type: "INTEGER", nullable: false),
                    IncidentRate = table.Column<double>(type: "REAL", nullable: false),
                    Suspects = table.Column<int>(type: "INTEGER", nullable: false),
                    CrimeRate = table.Column<double>(type: "REAL", nullable: false),
                    SolvedTotal = table.Column<int>(type: "INTEGER", nullable: false),
                    SolvedCurrent = table.Column<int>(type: "INTEGER", nullable: false),
                    SolvedBacklog = table.Column<int>(type: "INTEGER", nullable: false),
                    SolvedOther = table.Column<int>(type: "INTEGER", nullable: false),
                    SolveRate = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrimeStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrimeStats");

            migrationBuilder.DropTable(
                name: "UserAccounts");
        }
    }
}
