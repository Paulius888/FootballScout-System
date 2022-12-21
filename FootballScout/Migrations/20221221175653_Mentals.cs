using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class Mentals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mental",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aggression = table.Column<int>(type: "int", nullable: false),
                    Anticipation = table.Column<int>(type: "int", nullable: false),
                    Bravery = table.Column<int>(type: "int", nullable: false),
                    Composure = table.Column<int>(type: "int", nullable: false),
                    Concentration = table.Column<int>(type: "int", nullable: false),
                    Decisions = table.Column<int>(type: "int", nullable: false),
                    Determination = table.Column<int>(type: "int", nullable: false),
                    Flair = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    OffTheBall = table.Column<int>(type: "int", nullable: false),
                    Positioning = table.Column<int>(type: "int", nullable: false),
                    Teamwork = table.Column<int>(type: "int", nullable: false),
                    Vision = table.Column<int>(type: "int", nullable: false),
                    WorkRate = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Mental_PlayerId",
                table: "Mental",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mental");
        }
    }
}
