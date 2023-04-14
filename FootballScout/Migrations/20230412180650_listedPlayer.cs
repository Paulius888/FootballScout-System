using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class listedPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListedPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Contract = table.Column<DateOnly>(type: "date", nullable: false),
                    Wage = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    CurrentAbility = table.Column<int>(type: "integer", nullable: false),
                    PotentialAbility = table.Column<int>(type: "integer", nullable: false),
                    IsGoalKeeper = table.Column<bool>(type: "boolean", nullable: false),
                    IsEuCitizen = table.Column<bool>(type: "boolean", nullable: false),
                    Personality = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string[]>(type: "text[]", nullable: false),
                    TeamName = table.Column<string>(name: "Team_Name", type: "text", nullable: false),
                    Playerid = table.Column<int>(name: "Player_id", type: "integer", nullable: false),
                    ShortListId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListedPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListedPlayer_ShortList_ShortListId",
                        column: x => x.ShortListId,
                        principalTable: "ShortList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListedPlayer_ShortListId",
                table: "ListedPlayer",
                column: "ShortListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListedPlayer");
        }
    }
}
