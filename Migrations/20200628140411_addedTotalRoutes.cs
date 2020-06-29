using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class addedTotalRoutes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "InteractionStats");

            migrationBuilder.AddColumn<int>(
                name: "TotalRoutes",
                table: "InteractionStats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalRoutes",
                table: "InteractionStats");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "InteractionStats",
                type: "text",
                nullable: true);
        }
    }
}
