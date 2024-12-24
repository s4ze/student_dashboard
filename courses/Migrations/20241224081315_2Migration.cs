using Microsoft.EntityFrameworkCore.Migrations;
using courses.Models;

#nullable disable

namespace courses.Migrations
{
    /// <inheritdoc />
    public partial class _2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<LessonDay>(
                name: "Friday",
                table: "Schedules",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<LessonDay>(
                name: "Monday",
                table: "Schedules",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<LessonDay>(
                name: "Saturday",
                table: "Schedules",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<LessonDay>(
                name: "Thursday",
                table: "Schedules",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<LessonDay>(
                name: "Tuesday",
                table: "Schedules",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<LessonDay>(
                name: "Wednesday",
                table: "Schedules",
                type: "jsonb",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Schedules");
        }
    }
}
