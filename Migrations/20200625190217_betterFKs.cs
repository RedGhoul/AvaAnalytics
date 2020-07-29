using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class betterFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrowserStats_WebSites_WebSiteId",
                table: "BrowserStats");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionCounts_WebSites_WebSiteId",
                table: "InteractionCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Sessions_SessionId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_WebSites_WebSiteId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionStats_WebSites_WebSiteId",
                table: "InteractionStats");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationStats_WebSites_WebSiteId",
                table: "LocationStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_WebSites_WebSiteId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemStats_WebSites_WebSiteId",
                table: "SystemStats");

            migrationBuilder.DropColumn(
                name: "InteractionStatsId",
                table: "InteractionStats");

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "SystemStats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "LocationStats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "InteractionStats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "Interactions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Interactions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "InteractionCounts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "BrowserStats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BrowserStats_WebSites_WebSiteId",
                table: "BrowserStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionCounts_WebSites_WebSiteId",
                table: "InteractionCounts",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Sessions_SessionId",
                table: "Interactions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_WebSites_WebSiteId",
                table: "Interactions",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionStats_WebSites_WebSiteId",
                table: "InteractionStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationStats_WebSites_WebSiteId",
                table: "LocationStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_WebSites_WebSiteId",
                table: "Sessions",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemStats_WebSites_WebSiteId",
                table: "SystemStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrowserStats_WebSites_WebSiteId",
                table: "BrowserStats");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionCounts_WebSites_WebSiteId",
                table: "InteractionCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Sessions_SessionId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_WebSites_WebSiteId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionStats_WebSites_WebSiteId",
                table: "InteractionStats");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationStats_WebSites_WebSiteId",
                table: "LocationStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_WebSites_WebSiteId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemStats_WebSites_WebSiteId",
                table: "SystemStats");

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "SystemStats",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "Sessions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "LocationStats",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "InteractionStats",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "InteractionStatsId",
                table: "InteractionStats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "Interactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Interactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "InteractionCounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WebSiteId",
                table: "BrowserStats",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BrowserStats_WebSites_WebSiteId",
                table: "BrowserStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionCounts_WebSites_WebSiteId",
                table: "InteractionCounts",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Sessions_SessionId",
                table: "Interactions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_WebSites_WebSiteId",
                table: "Interactions",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionStats_WebSites_WebSiteId",
                table: "InteractionStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationStats_WebSites_WebSiteId",
                table: "LocationStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_WebSites_WebSiteId",
                table: "Sessions",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemStats_WebSites_WebSiteId",
                table: "SystemStats",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
