using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courses.Migrations
{
    /// <inheritdoc />
    public partial class _4Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Grade",
                table: "Enrollments",
                type: "numeric(5,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "numeric(3,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Grade",
                table: "Enrollments",
                type: "numeric(3,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "numeric(5,2)");
        }
    }
}
