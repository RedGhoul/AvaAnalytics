using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SharpCounter.Migrations
{
    public partial class changeHourToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InteractionCounts_Hour",
                table: "InteractionCounts");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "InteractionCounts");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "InteractionCounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_Date",
                table: "InteractionCounts",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InteractionCounts_Date",
                table: "InteractionCounts");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "InteractionCounts");

            migrationBuilder.AddColumn<DateTime>(
                name: "Hour",
                table: "InteractionCounts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_Hour",
                table: "InteractionCounts",
                column: "Hour");
        }
    }
}
