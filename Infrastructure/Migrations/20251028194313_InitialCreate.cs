using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamWinsWithMascots.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Mascot = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    DateOfLastWin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    WinsPercentage = table.Column<decimal>(type: "TEXT", nullable: false),
                    Wins = table.Column<int>(type: "INTEGER", nullable: false),
                    Losses = table.Column<int>(type: "INTEGER", nullable: false),
                    Ties = table.Column<int>(type: "INTEGER", nullable: false),
                    Games = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
