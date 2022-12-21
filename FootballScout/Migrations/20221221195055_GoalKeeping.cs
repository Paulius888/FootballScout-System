using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class GoalKeeping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goalkeeping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AerialReach = table.Column<int>(type: "int", nullable: false),
                    CommandOfArea = table.Column<int>(type: "int", nullable: false),
                    Communication = table.Column<int>(type: "int", nullable: false),
                    Eccentricity = table.Column<int>(type: "int", nullable: false),
                    FirstTouch = table.Column<int>(type: "int", nullable: false),
                    Handling = table.Column<int>(type: "int", nullable: false),
                    Kicking = table.Column<int>(type: "int", nullable: false),
                    OneOnOnes = table.Column<int>(type: "int", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: false),
                    Punching = table.Column<int>(type: "int", nullable: false),
                    Reflexes = table.Column<int>(type: "int", nullable: false),
                    RushingOut = table.Column<int>(type: "int", nullable: false),
                    Throwing = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Goalkeeping_PlayerId",
                table: "Goalkeeping",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goalkeeping");
        }
    }
}
