using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Presentation.Migrations
{
    public partial class addedTimeZones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "UserSettings");

            migrationBuilder.CreateTable(
                name: "TimeZoneValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "longtext", nullable: true),
                    UserSettingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZoneValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeZoneValues_UserSettings_UserSettingId",
                        column: x => x.UserSettingId,
                        principalTable: "UserSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeZoneValues_UserSettingId",
                table: "TimeZoneValues",
                column: "UserSettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeZoneValues");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "UserSettings",
                type: "longtext",
                nullable: true);
        }
    }
}
