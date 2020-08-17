using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
    public partial class addedSiteContents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LargeHeader = table.Column<string>(nullable: true),
                    BlurgUnderLargerHeader = table.Column<string>(nullable: true),
                    Header1 = table.Column<string>(nullable: true),
                    Header1Content = table.Column<string>(nullable: true),
                    Header2 = table.Column<string>(nullable: true),
                    Header2Content = table.Column<string>(nullable: true),
                    Header3 = table.Column<string>(nullable: true),
                    Header3Content = table.Column<string>(nullable: true),
                    FinalHeader = table.Column<string>(nullable: true),
                    FinalHeaderContent = table.Column<string>(nullable: true),
                    WhatWeCollect = table.Column<string>(nullable: true),
                    PrivacyPolicy = table.Column<string>(nullable: true),
                    Documentation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteContents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteContents");
        }
    }
}
