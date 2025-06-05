using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class _003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RescueDatas");

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    EnabledFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnabledTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RemoteId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardAccessGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardAccessGrants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardAccessGrants_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardAccessGrants_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardAccessGrants_CardId",
                table: "CardAccessGrants",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardAccessGrants_DeviceId",
                table: "CardAccessGrants",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardAccessGrants");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.CreateTable(
                name: "RescueDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcuteDisease = table.Column<int>(type: "int", nullable: false),
                    AnimalBite = table.Column<int>(type: "int", nullable: false),
                    Burn = table.Column<int>(type: "int", nullable: false),
                    CO_Poisoning = table.Column<int>(type: "int", nullable: false),
                    Collapse = table.Column<int>(type: "int", nullable: false),
                    Drowning = table.Column<int>(type: "int", nullable: false),
                    DrugPoisoning = table.Column<int>(type: "int", nullable: false),
                    ElectricShock = table.Column<int>(type: "int", nullable: false),
                    FallInjury = table.Column<int>(type: "int", nullable: false),
                    Falling = table.Column<int>(type: "int", nullable: false),
                    GeneralAmbulance = table.Column<int>(type: "int", nullable: false),
                    GeneralTrauma = table.Column<int>(type: "int", nullable: false),
                    ICUAmbulance = table.Column<int>(type: "int", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MentalDisorder = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    NonTrauma_OHCA = table.Column<int>(type: "int", nullable: false),
                    NonTrauma_Other = table.Column<int>(type: "int", nullable: false),
                    NotTransported = table.Column<int>(type: "int", nullable: false),
                    PregnancyEmergency = table.Column<int>(type: "int", nullable: false),
                    Seizure = table.Column<int>(type: "int", nullable: false),
                    StabWound = table.Column<int>(type: "int", nullable: false),
                    TrafficInjury = table.Column<int>(type: "int", nullable: false),
                    Transported = table.Column<int>(type: "int", nullable: false),
                    Trauma_OHCA = table.Column<int>(type: "int", nullable: false),
                    Trauma_Other = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RescueDatas", x => x.Id);
                });
        }
    }
}
