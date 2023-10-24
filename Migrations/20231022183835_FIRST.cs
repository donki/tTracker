using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tTrackerWeb.Migrations
{
    /// <inheritdoc />
    public partial class FIRST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSegments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSegments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    MorningStartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MorningEndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AfternoonStartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AfternoonEndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaxEntryTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BreakDuration = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    MaxWorkHours = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSegments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
