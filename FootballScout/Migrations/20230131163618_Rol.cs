using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class Rol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "Role",
                table: "Player",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Player");
        }
    }
}
