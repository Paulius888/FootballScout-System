using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class GoalSats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoalStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ShotStoping = table.Column<int>(type: "integer", nullable: false),
                    Physicals = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Communication = table.Column<int>(type: "integer", nullable: false),
                    Eccentricity = table.Column<int>(type: "integer", nullable: false),
                    Distribution = table.Column<int>(type: "integer", nullable: false),
                    Aerial = table.Column<int>(type: "integer", nullable: false),
                    Mentals = table.Column<int>(type: "integer", nullable: false),
                    Overall = table.Column<int>(type: "integer", nullable: false),
                    GoalkeepingId = table.Column<int>(type: "integer", nullable: false),
                    MentalId = table.Column<int>(type: "integer", nullable: false),
                    PhysicalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalStats_Goalkeeping_GoalkeepingId",
                        column: x => x.GoalkeepingId,
                        principalTable: "Goalkeeping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalStats_Mental_MentalId",
                        column: x => x.MentalId,
                        principalTable: "Mental",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoalStats_Physical_PhysicalId",
                        column: x => x.PhysicalId,
                        principalTable: "Physical",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalStats_GoalkeepingId",
                table: "GoalStats",
                column: "GoalkeepingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoalStats_MentalId",
                table: "GoalStats",
                column: "MentalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoalStats_PhysicalId",
                table: "GoalStats",
                column: "PhysicalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoalStats");
        }
    }
}
