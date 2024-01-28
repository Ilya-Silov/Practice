using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace practice.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "silov-barinov-maltsev");

            migrationBuilder.CreateTable(
                name: "Cites",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HyperAddress = table.Column<string>(type: "text", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryName = table.Column<string>(type: "text", nullable: false),
                    CountryEngName = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CodeNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directions",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronomic = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CountryID = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    DirectionId = table.Column<int>(type: "integer", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Directions_DirectionId",
                        column: x => x.DirectionId,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Directions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ivents",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<int>(type: "integer", nullable: false),
                    DateBegin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AmountDays = table.Column<int>(type: "integer", nullable: false),
                    Photo = table.Column<string>(type: "text", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    WinnerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ivents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ivents_Cites_CityId",
                        column: x => x.CityId,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Cites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ivents_Users_WinnerId",
                        column: x => x.WinnerId,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Activites",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    TimeBegin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModeratorName = table.Column<string>(type: "text", nullable: false),
                    ModeratorId = table.Column<int>(type: "integer", nullable: false),
                    IventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activites", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Activites_Ivents_IventId",
                        column: x => x.IventId,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Ivents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activites_Users_ModeratorId",
                        column: x => x.ModeratorId,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityJures",
                schema: "silov-barinov-maltsev",
                columns: table => new
                {
                    JuryID = table.Column<int>(type: "integer", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityJures", x => new { x.JuryID, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_ActivityJures_Activites_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Activites",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityJures_Users_JuryID",
                        column: x => x.JuryID,
                        principalSchema: "silov-barinov-maltsev",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activites_IventId",
                schema: "silov-barinov-maltsev",
                table: "Activites",
                column: "IventId");

            migrationBuilder.CreateIndex(
                name: "IX_Activites_ModeratorId",
                schema: "silov-barinov-maltsev",
                table: "Activites",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityJures_ActivityId",
                schema: "silov-barinov-maltsev",
                table: "ActivityJures",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ivents_CityId",
                schema: "silov-barinov-maltsev",
                table: "Ivents",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ivents_WinnerId",
                schema: "silov-barinov-maltsev",
                table: "Ivents",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryID",
                schema: "silov-barinov-maltsev",
                table: "Users",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DirectionId",
                schema: "silov-barinov-maltsev",
                table: "Users",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "silov-barinov-maltsev",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityJures",
                schema: "silov-barinov-maltsev");

            migrationBuilder.DropTable(
                name: "Activites",
                schema: "silov-barinov-maltsev");

            migrationBuilder.DropTable(
                name: "Ivents",
                schema: "silov-barinov-maltsev");

            migrationBuilder.DropTable(
                name: "Cites",
                schema: "silov-barinov-maltsev");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "silov-barinov-maltsev");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "silov-barinov-maltsev");

            migrationBuilder.DropTable(
                name: "Directions",
                schema: "silov-barinov-maltsev");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "silov-barinov-maltsev");
        }
    }
}
