using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authorization.Migrations
{
    /// <inheritdoc />
    public partial class _2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "VARCHAR(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(128)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "VARCHAR(128)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(128)",
                oldNullable: true);
        }
    }
}
