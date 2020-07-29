using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class removedValuesFromInterCounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "InteractionCounts");

            migrationBuilder.DropColumn(
                name: "TotalUnique",
                table: "InteractionCounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "InteractionCounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalUnique",
                table: "InteractionCounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
