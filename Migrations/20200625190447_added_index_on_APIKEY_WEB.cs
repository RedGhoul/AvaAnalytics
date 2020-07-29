using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class added_index_on_APIKEY_WEB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WebSites_APIKey",
                table: "WebSites",
                column: "APIKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WebSites_APIKey",
                table: "WebSites");
        }
    }
}
