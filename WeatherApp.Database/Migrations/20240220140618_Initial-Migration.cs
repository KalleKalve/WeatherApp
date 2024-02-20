using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Database.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemperatureC = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SavedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherSnapshots", x => x.Id);
                });

            migrationBuilder.InsertData( // for Demo purpose, since API data refresh sometimes is 15 minutes.
                table: "WeatherSnapshots",
                columns: new[] { "Id", "City", "Country", "LastUpdate", "SavedOn", "TemperatureC" },
                values: new object[,]
                {
                    { 1, "Riga", "Latvia", new DateTime(2024, 2, 20, 10, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 20, 14, 6, 18, 112, DateTimeKind.Utc).AddTicks(6754), 1.5m },
                    { 2, "Tallinn", "Estonia", new DateTime(2024, 2, 20, 10, 50, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 20, 14, 6, 18, 112, DateTimeKind.Utc).AddTicks(6756), 1.0m },
                    { 3, "Riga", "Latvia", new DateTime(2024, 2, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 20, 14, 16, 18, 112, DateTimeKind.Utc).AddTicks(6758), -0.5m },
                    { 4, "Tallinn", "Estonia", new DateTime(2024, 2, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 20, 14, 16, 18, 112, DateTimeKind.Utc).AddTicks(6764), -1.0m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherSnapshots");
        }
    }
}
