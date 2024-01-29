using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace practice.Migrations
{
    /// <inheritdoc />
    public partial class dвывыфв : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HyperAddress",
                schema: "silov-barinov-maltsev",
                table: "Cites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HyperAddress",
                schema: "silov-barinov-maltsev",
                table: "Cites",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
