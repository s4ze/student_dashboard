using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authorization.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(513)", nullable: false),
                    Role = table.Column<string>(type: "VARCHAR(16)", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(256)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    Contact = table.Column<string>(type: "TEXT", nullable: false),
                    Group = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    RefreshToken = table.Column<string>(type: "VARCHAR(128)", nullable: false),
                    CreatedAt = table.Column<string>(type: "VARCHAR(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
