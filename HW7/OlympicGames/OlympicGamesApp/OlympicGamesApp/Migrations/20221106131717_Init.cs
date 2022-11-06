using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OlympicGamesApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    country_id = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    area_sqkm = table.Column<int>(type: "integer", nullable: false),
                    population = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "Olympics",
                columns: table => new
                {
                    olympic_id = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    country_id = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    startdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Olympics", x => x.olympic_id);
                    table.ForeignKey(
                        name: "FK_Olympics_Countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    player_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    country_id = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.player_id);
                    table.ForeignKey(
                        name: "FK_Players_Countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    event_id = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    eventtype = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    olympic_id = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    is_team_event = table.Column<bool>(type: "boolean", nullable: false),
                    num_players_in_team = table.Column<int>(type: "integer", nullable: false),
                    result_noted_in = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_Events_Olympics_olympic_id",
                        column: x => x.olympic_id,
                        principalTable: "Olympics",
                        principalColumn: "olympic_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    result_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    player_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    medal = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    result = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.result_id);
                    table.ForeignKey(
                        name: "FK_Results_Events_event_id",
                        column: x => x.event_id,
                        principalTable: "Events",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Players_player_id",
                        column: x => x.player_id,
                        principalTable: "Players",
                        principalColumn: "player_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_olympic_id",
                table: "Events",
                column: "olympic_id");

            migrationBuilder.CreateIndex(
                name: "IX_Olympics_country_id",
                table: "Olympics",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_country_id",
                table: "Players",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_Results_event_id",
                table: "Results",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_Results_player_id",
                table: "Results",
                column: "player_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Olympics");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
