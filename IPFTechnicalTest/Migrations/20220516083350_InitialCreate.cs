using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPFTechnicalTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bar",
                columns: table => new
                {
                    BarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bar", x => x.BarId);
                });

            migrationBuilder.CreateTable(
                name: "Brewery",
                columns: table => new
                {
                    BreweryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewery", x => x.BreweryId);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    BeerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PercentageAlcoholByVolume = table.Column<decimal>(type: "TEXT", nullable: false),
                    BarId = table.Column<int>(type: "INTEGER", nullable: true),
                    BreweryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beer", x => x.BeerId);
                    table.ForeignKey(
                        name: "FK_Beer_Bar_BarId",
                        column: x => x.BarId,
                        principalTable: "Bar",
                        principalColumn: "BarId");
                    table.ForeignKey(
                        name: "FK_Beer_Brewery_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Brewery",
                        principalColumn: "BreweryId");
                });

            migrationBuilder.InsertData(
                table: "Bar",
                columns: new[] { "BarId", "Address", "Name" },
                values: new object[] { 1, "London", "All Bar One" });

            migrationBuilder.InsertData(
                table: "Bar",
                columns: new[] { "BarId", "Address", "Name" },
                values: new object[] { 2, "Amersham", "The Pomeroy" });

            migrationBuilder.InsertData(
                table: "Beer",
                columns: new[] { "BeerId", "BarId", "BreweryId", "Name", "PercentageAlcoholByVolume" },
                values: new object[] { 1, null, null, "Corona", 4.5m });

            migrationBuilder.InsertData(
                table: "Beer",
                columns: new[] { "BeerId", "BarId", "BreweryId", "Name", "PercentageAlcoholByVolume" },
                values: new object[] { 2, null, null, "Modelo", 4m });

            migrationBuilder.InsertData(
                table: "Beer",
                columns: new[] { "BeerId", "BarId", "BreweryId", "Name", "PercentageAlcoholByVolume" },
                values: new object[] { 3, null, null, "Pacifico", 3.5m });

            migrationBuilder.InsertData(
                table: "Beer",
                columns: new[] { "BeerId", "BarId", "BreweryId", "Name", "PercentageAlcoholByVolume" },
                values: new object[] { 4, null, null, "Heineken", 4.7m });

            migrationBuilder.InsertData(
                table: "Beer",
                columns: new[] { "BeerId", "BarId", "BreweryId", "Name", "PercentageAlcoholByVolume" },
                values: new object[] { 5, null, null, "Amstel", 4.8m });

            migrationBuilder.InsertData(
                table: "Brewery",
                columns: new[] { "BreweryId", "Name" },
                values: new object[] { 1, "Grupo Modelo" });

            migrationBuilder.InsertData(
                table: "Brewery",
                columns: new[] { "BreweryId", "Name" },
                values: new object[] { 2, "Heineken N.V." });

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BarId",
                table: "Beer",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreweryId",
                table: "Beer",
                column: "BreweryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "Bar");

            migrationBuilder.DropTable(
                name: "Brewery");
        }
    }
}
