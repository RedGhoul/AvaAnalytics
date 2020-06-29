using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpCounter.Migrations
{
    public partial class addedNewRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InteractionStatsId",
                table: "InteractionCounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InteractionStats_Date",
                table: "InteractionStats",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_InteractionStatsId",
                table: "InteractionCounts",
                column: "InteractionStatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionCounts_InteractionStats_InteractionStatsId",
                table: "InteractionCounts",
                column: "InteractionStatsId",
                principalTable: "InteractionStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InteractionCounts_InteractionStats_InteractionStatsId",
                table: "InteractionCounts");

            migrationBuilder.DropIndex(
                name: "IX_InteractionStats_Date",
                table: "InteractionStats");

            migrationBuilder.DropIndex(
                name: "IX_InteractionCounts_InteractionStatsId",
                table: "InteractionCounts");

            migrationBuilder.DropColumn(
                name: "InteractionStatsId",
                table: "InteractionCounts");
        }
    }
}
