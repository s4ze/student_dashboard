using Microsoft.EntityFrameworkCore.Migrations;
using courses.Models;

#nullable disable

namespace courses.Migrations
{
    /// <inheritdoc />
    public partial class _5Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Wednesday",
                table: "Schedules",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(LessonDay),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Tuesday",
                table: "Schedules",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(LessonDay),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Thursday",
                table: "Schedules",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(LessonDay),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Saturday",
                table: "Schedules",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(LessonDay),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Monday",
                table: "Schedules",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(LessonDay),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Friday",
                table: "Schedules",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(LessonDay),
                oldType: "jsonb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<LessonDay>(
                name: "Wednesday",
                table: "Schedules",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<LessonDay>(
                name: "Tuesday",
                table: "Schedules",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<LessonDay>(
                name: "Thursday",
                table: "Schedules",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<LessonDay>(
                name: "Saturday",
                table: "Schedules",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<LessonDay>(
                name: "Monday",
                table: "Schedules",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<LessonDay>(
                name: "Friday",
                table: "Schedules",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
