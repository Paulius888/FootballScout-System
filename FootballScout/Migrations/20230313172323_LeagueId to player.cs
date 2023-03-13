using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class LeagueIdtoplayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Player");
        }
    }
}
