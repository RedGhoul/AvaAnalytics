using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class added_things : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionUId",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Interactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referrer",
                table: "Interactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionUId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "Referrer",
                table: "Interactions");
        }
    }
}
