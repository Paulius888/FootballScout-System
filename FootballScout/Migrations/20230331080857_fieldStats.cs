using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FootballScout.Migrations
{
    /// <inheritdoc />
    public partial class fieldStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Defending = table.Column<int>(type: "integer", nullable: false),
                    Physicals = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    Vision = table.Column<int>(type: "integer", nullable: false),
                    Attacking = table.Column<int>(type: "integer", nullable: false),
                    Technicals = table.Column<int>(type: "integer", nullable: false),
                    Aerial = table.Column<int>(type: "integer", nullable: false),
                    Mentals = table.Column<int>(type: "integer", nullable: false),
                    Overall = table.Column<int>(type: "integer", nullable: false),
                    TechnicalId = table.Column<int>(type: "integer", nullable: false),
                    MentalId = table.Column<int>(type: "integer", nullable: false),
                    PhysicalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldStats_Mental_MentalId",
                        column: x => x.MentalId,
                        principalTable: "Mental",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldStats_Physical_PhysicalId",
                        column: x => x.PhysicalId,
                        principalTable: "Physical",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldStats_Technical_TechnicalId",
                        column: x => x.TechnicalId,
                        principalTable: "Technical",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldStats_MentalId",
                table: "FieldStats",
                column: "MentalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldStats_PhysicalId",
                table: "FieldStats",
                column: "PhysicalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldStats_TechnicalId",
                table: "FieldStats",
                column: "TechnicalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldStats");
        }
    }
}
