using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaptimeBaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemadeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    @class = table.Column<string>(name: "class", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "track",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_track", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    is_admin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "session",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    held_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ambient_temp = table.Column<int>(type: "INTEGER", nullable: false),
                    track_temp = table.Column<int>(type: "INTEGER", nullable: false),
                    TrackId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session", x => x.id);
                    table.ForeignKey(
                        name: "FK_session_track_TrackId",
                        column: x => x.TrackId,
                        principalTable: "track",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_team_car_CarId",
                        column: x => x.CarId,
                        principalTable: "car",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "laptime",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    time = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptime", x => x.id);
                    table.ForeignKey(
                        name: "FK_laptime_session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "session",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_laptime_team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_laptime_SessionId",
                table: "laptime",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_laptime_TeamId",
                table: "laptime",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_session_TrackId",
                table: "session",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_team_CarId",
                table: "team",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_team_UserId",
                table: "team",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "laptime");

            migrationBuilder.DropTable(
                name: "session");

            migrationBuilder.DropTable(
                name: "team");

            migrationBuilder.DropTable(
                name: "track");

            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
