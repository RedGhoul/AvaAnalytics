using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class addedScreenSizeToInteractions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "DevicePixelRatio",
                table: "Interactions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ScreenHeight",
                table: "Interactions",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ScreenWidth",
                table: "Interactions",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevicePixelRatio",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "ScreenHeight",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "ScreenWidth",
                table: "Interactions");
        }
    }
}
