using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace SharpCounter.Migrations
{
    public partial class addedScreenSizeStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScreenSizeStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    NumberOfPhones = table.Column<int>(nullable: false),
                    LargePhonesSmallTablets = table.Column<int>(nullable: false),
                    TabletsSmallLaptops = table.Column<int>(nullable: false),
                    ComputerMonitors = table.Column<int>(nullable: false),
                    ComputerMonitors4K = table.Column<int>(nullable: false),
                    WebSiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenSizeStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenSizeStats_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_DevicePixelRatio",
                table: "Interactions",
                column: "DevicePixelRatio");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_ScreenHeight",
                table: "Interactions",
                column: "ScreenHeight");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_ScreenWidth",
                table: "Interactions",
                column: "ScreenWidth");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSizeStats_Date",
                table: "ScreenSizeStats",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSizeStats_WebSiteId",
                table: "ScreenSizeStats",
                column: "WebSiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreenSizeStats");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_DevicePixelRatio",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_ScreenHeight",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_ScreenWidth",
                table: "Interactions");
        }
    }
}
