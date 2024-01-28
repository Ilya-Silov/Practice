using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace practice.Migrations
{
    /// <inheritdoc />
    public partial class ndaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryName",
                schema: "silov-barinov-maltsev",
                table: "Countries",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CountryEngName",
                schema: "silov-barinov-maltsev",
                table: "Countries",
                newName: "EngName");

            migrationBuilder.RenameColumn(
                name: "CityName",
                schema: "silov-barinov-maltsev",
                table: "Cites",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "silov-barinov-maltsev",
                table: "Countries",
                newName: "CountryName");

            migrationBuilder.RenameColumn(
                name: "EngName",
                schema: "silov-barinov-maltsev",
                table: "Countries",
                newName: "CountryEngName");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "silov-barinov-maltsev",
                table: "Cites",
                newName: "CityName");
        }
    }
}
