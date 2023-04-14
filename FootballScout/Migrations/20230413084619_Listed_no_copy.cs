using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class Listednocopy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "Contract",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "CurrentAbility",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "IsEuCitizen",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "IsGoalKeeper",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "Personality",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "Player_id",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "PotentialAbility",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ListedPlayer");

            migrationBuilder.DropColumn(
                name: "Team_Name",
                table: "ListedPlayer");

            migrationBuilder.RenameColumn(
                name: "Wage",
                table: "ListedPlayer",
                newName: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ListedPlayer_PlayerId",
                table: "ListedPlayer",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListedPlayer_Player_PlayerId",
                table: "ListedPlayer",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListedPlayer_Player_PlayerId",
                table: "ListedPlayer");

            migrationBuilder.DropIndex(
                name: "IX_ListedPlayer_PlayerId",
                table: "ListedPlayer");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "ListedPlayer",
                newName: "Wage");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "ListedPlayer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Contract",
                table: "ListedPlayer",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "CurrentAbility",
                table: "ListedPlayer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEuCitizen",
                table: "ListedPlayer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGoalKeeper",
                table: "ListedPlayer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ListedPlayer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Personality",
                table: "ListedPlayer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Player_id",
                table: "ListedPlayer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PotentialAbility",
                table: "ListedPlayer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ListedPlayer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string[]>(
                name: "Role",
                table: "ListedPlayer",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string>(
                name: "Team_Name",
                table: "ListedPlayer",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
