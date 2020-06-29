using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class addedProperFKToWebsite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebSites_AspNetUsers_OwnerId",
                table: "WebSites");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "WebSites",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WebSites_AspNetUsers_OwnerId",
                table: "WebSites",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebSites_AspNetUsers_OwnerId",
                table: "WebSites");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "WebSites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_WebSites_AspNetUsers_OwnerId",
                table: "WebSites",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
