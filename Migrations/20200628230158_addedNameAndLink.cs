using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class addedNameAndLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkDomain",
                table: "WebSites");

            migrationBuilder.AddColumn<string>(
                name: "HomePageLink",
                table: "WebSites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WebSites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomePageLink",
                table: "WebSites");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WebSites");

            migrationBuilder.AddColumn<string>(
                name: "LinkDomain",
                table: "WebSites",
                type: "text",
                nullable: true);
        }
    }
}
