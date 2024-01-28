using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace practice.Migrations
{
    /// <inheritdoc />
    public partial class weqwe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityJures",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityJures",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityJures_JuryID",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures",
                column: "JuryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityJures",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures");

            migrationBuilder.DropIndex(
                name: "IX_ActivityJures_JuryID",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityJures",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures",
                columns: new[] { "JuryID", "ActivityId" });
        }
    }
}
