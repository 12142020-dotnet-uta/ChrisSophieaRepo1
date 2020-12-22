using Microsoft.EntityFrameworkCore.Migrations;

namespace RpsGameWithDb.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numLosses",
                table: "players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numWins",
                table: "players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "p1RoundWins",
                table: "matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "p2RoundWins",
                table: "matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ties",
                table: "matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numLosses",
                table: "players");

            migrationBuilder.DropColumn(
                name: "numWins",
                table: "players");

            migrationBuilder.DropColumn(
                name: "p1RoundWins",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "p2RoundWins",
                table: "matches");

            migrationBuilder.DropColumn(
                name: "ties",
                table: "matches");
        }
    }
}
