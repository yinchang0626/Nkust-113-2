using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RescueDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    GeneralAmbulance = table.Column<int>(type: "int", nullable: false),
                    ICUAmbulance = table.Column<int>(type: "int", nullable: false),
                    Transported = table.Column<int>(type: "int", nullable: false),
                    NotTransported = table.Column<int>(type: "int", nullable: false),
                    AcuteDisease = table.Column<int>(type: "int", nullable: false),
                    DrugPoisoning = table.Column<int>(type: "int", nullable: false),
                    CO_Poisoning = table.Column<int>(type: "int", nullable: false),
                    Seizure = table.Column<int>(type: "int", nullable: false),
                    Collapse = table.Column<int>(type: "int", nullable: false),
                    MentalDisorder = table.Column<int>(type: "int", nullable: false),
                    PregnancyEmergency = table.Column<int>(type: "int", nullable: false),
                    NonTrauma_OHCA = table.Column<int>(type: "int", nullable: false),
                    NonTrauma_Other = table.Column<int>(type: "int", nullable: false),
                    GeneralTrauma = table.Column<int>(type: "int", nullable: false),
                    TrafficInjury = table.Column<int>(type: "int", nullable: false),
                    Drowning = table.Column<int>(type: "int", nullable: false),
                    FallInjury = table.Column<int>(type: "int", nullable: false),
                    Falling = table.Column<int>(type: "int", nullable: false),
                    StabWound = table.Column<int>(type: "int", nullable: false),
                    Burn = table.Column<int>(type: "int", nullable: false),
                    ElectricShock = table.Column<int>(type: "int", nullable: false),
                    AnimalBite = table.Column<int>(type: "int", nullable: false),
                    Trauma_OHCA = table.Column<int>(type: "int", nullable: false),
                    Trauma_Other = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RescueDatas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RescueDatas");
        }
    }
}
