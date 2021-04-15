using Microsoft.EntityFrameworkCore.Migrations;

namespace Presentation.Migrations
{
    public partial class added_demo_col : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DemoSite",
                table: "WebSites",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemoSite",
                table: "WebSites");
        }
    }
}
