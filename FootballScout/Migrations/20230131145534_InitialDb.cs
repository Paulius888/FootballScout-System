using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Nation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TrainingFacilities = table.Column<string>(name: "Training_Facilities", type: "text", nullable: false),
                    YouthFacilities = table.Column<string>(name: "Youth_Facilities", type: "text", nullable: false),
                    LeagueName = table.Column<string>(name: "League_Name", type: "text", nullable: false),
                    LeagueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Contract = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Wage = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    CurrentAbility = table.Column<int>(type: "integer", nullable: false),
                    PotentialAbility = table.Column<int>(type: "integer", nullable: false),
                    IsGoalKeeper = table.Column<bool>(type: "boolean", nullable: false),
                    IsEuCitizen = table.Column<bool>(type: "boolean", nullable: false),
                    Personality = table.Column<string>(type: "text", nullable: false),
                    TeamName = table.Column<string>(name: "Team_Name", type: "text", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goalkeeping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AerialReach = table.Column<int>(type: "integer", nullable: false),
                    CommandOfArea = table.Column<int>(type: "integer", nullable: false),
                    Communication = table.Column<int>(type: "integer", nullable: false),
                    Eccentricity = table.Column<int>(type: "integer", nullable: false),
                    FirstTouch = table.Column<int>(type: "integer", nullable: false),
                    Handling = table.Column<int>(type: "integer", nullable: false),
                    Kicking = table.Column<int>(type: "integer", nullable: false),
                    OneOnOnes = table.Column<int>(type: "integer", nullable: false),
                    Passing = table.Column<int>(type: "integer", nullable: false),
                    Punching = table.Column<int>(type: "integer", nullable: false),
                    Reflexes = table.Column<int>(type: "integer", nullable: false),
                    RushingOut = table.Column<int>(type: "integer", nullable: false),
                    Throwing = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goalkeeping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goalkeeping_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mental",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aggression = table.Column<int>(type: "integer", nullable: false),
                    Anticipation = table.Column<int>(type: "integer", nullable: false),
                    Bravery = table.Column<int>(type: "integer", nullable: false),
                    Composure = table.Column<int>(type: "integer", nullable: false),
                    Concentration = table.Column<int>(type: "integer", nullable: false),
                    Decisions = table.Column<int>(type: "integer", nullable: false),
                    Determination = table.Column<int>(type: "integer", nullable: false),
                    Flair = table.Column<int>(type: "integer", nullable: false),
                    Leadership = table.Column<int>(type: "integer", nullable: false),
                    OffTheBall = table.Column<int>(type: "integer", nullable: false),
                    Positioning = table.Column<int>(type: "integer", nullable: false),
                    Teamwork = table.Column<int>(type: "integer", nullable: false),
                    Vision = table.Column<int>(type: "integer", nullable: false),
                    WorkRate = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mental", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mental_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Physical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Acceleration = table.Column<int>(type: "integer", nullable: false),
                    Agility = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<int>(type: "integer", nullable: false),
                    JumpingReach = table.Column<int>(type: "integer", nullable: false),
                    NaturalFitness = table.Column<int>(type: "integer", nullable: false),
                    Pace = table.Column<int>(type: "integer", nullable: false),
                    Stamina = table.Column<int>(type: "integer", nullable: false),
                    Strength = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Physical_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Corners = table.Column<int>(type: "integer", nullable: false),
                    Crossing = table.Column<int>(type: "integer", nullable: false),
                    Dribbling = table.Column<int>(type: "integer", nullable: false),
                    Finishing = table.Column<int>(type: "integer", nullable: false),
                    FirstTouch = table.Column<int>(type: "integer", nullable: false),
                    FreeKickTaking = table.Column<int>(type: "integer", nullable: false),
                    Heading = table.Column<int>(type: "integer", nullable: false),
                    LongShots = table.Column<int>(type: "integer", nullable: false),
                    LongThrows = table.Column<int>(type: "integer", nullable: false),
                    Marking = table.Column<int>(type: "integer", nullable: false),
                    Passing = table.Column<int>(type: "integer", nullable: false),
                    PenaltyTaking = table.Column<int>(type: "integer", nullable: false),
                    Tackling = table.Column<int>(type: "integer", nullable: false),
                    Technique = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technical_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goalkeeping_PlayerId",
                table: "Goalkeeping",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mental_PlayerId",
                table: "Mental",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Physical_PlayerId",
                table: "Physical",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId",
                table: "Player",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_LeagueId",
                table: "Team",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Technical_PlayerId",
                table: "Technical",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goalkeeping");

            migrationBuilder.DropTable(
                name: "Mental");

            migrationBuilder.DropTable(
                name: "Physical");

            migrationBuilder.DropTable(
                name: "Technical");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
