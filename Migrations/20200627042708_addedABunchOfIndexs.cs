using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class addedABunchOfIndexs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SystemStats_Day",
                table: "SystemStats",
                column: "Day");

            migrationBuilder.CreateIndex(
                name: "IX_SystemStats_Platform",
                table: "SystemStats",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CreatedAt",
                table: "Sessions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_LastSeen",
                table: "Sessions",
                column: "LastSeen");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionUId",
                table: "Sessions",
                column: "SessionUId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_Browser",
                table: "Interactions",
                column: "Browser");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_CreatedAt",
                table: "Interactions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_FirstVisit",
                table: "Interactions",
                column: "FirstVisit");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_Path",
                table: "Interactions",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_Hour",
                table: "InteractionCounts",
                column: "Hour");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_Path",
                table: "InteractionCounts",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_BrowserStats_Browser",
                table: "BrowserStats",
                column: "Browser");

            migrationBuilder.CreateIndex(
                name: "IX_BrowserStats_Date",
                table: "BrowserStats",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SystemStats_Day",
                table: "SystemStats");

            migrationBuilder.DropIndex(
                name: "IX_SystemStats_Platform",
                table: "SystemStats");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CreatedAt",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_LastSeen",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_SessionUId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_Browser",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_CreatedAt",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_FirstVisit",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_Path",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_InteractionCounts_Hour",
                table: "InteractionCounts");

            migrationBuilder.DropIndex(
                name: "IX_InteractionCounts_Path",
                table: "InteractionCounts");

            migrationBuilder.DropIndex(
                name: "IX_BrowserStats_Browser",
                table: "BrowserStats");

            migrationBuilder.DropIndex(
                name: "IX_BrowserStats_Date",
                table: "BrowserStats");
        }
    }
}
