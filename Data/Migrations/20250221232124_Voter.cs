using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BallonDor.Data.Migrations
{
    /// <inheritdoc />
    public partial class Voter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    VoterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.VoterId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Awards_VoterId",
                table: "Awards",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_Voters_VoterId",
                table: "Awards",
                column: "VoterId",
                principalTable: "Voters",
                principalColumn: "VoterId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awards_Voters_VoterId",
                table: "Awards");

            migrationBuilder.DropTable(
                name: "Voters");

            migrationBuilder.DropIndex(
                name: "IX_Awards_VoterId",
                table: "Awards");
        }
    }
}
