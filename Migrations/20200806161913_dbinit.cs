using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SharpCounter.Migrations
{
    public partial class dbinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebSites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    APIKey = table.Column<string>(nullable: true),
                    HomePageLink = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSites_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrowserStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Browser = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    CountUnique = table.Column<int>(nullable: false),
                    WebSiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrowserStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrowserStats_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteractionStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    WebSiteId = table.Column<int>(nullable: false),
                    TotalRoutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteractionStats_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WebSiteId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    CountUnique = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationStats_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageViewStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Count = table.Column<double>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    WebSiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageViewStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageViewStats_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionUId = table.Column<string>(nullable: true),
                    WebSiteId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastSeen = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<DateTime>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    CountUnique = table.Column<int>(nullable: false),
                    WebSiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemStats_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteractionCounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WebSiteId = table.Column<int>(nullable: false),
                    InteractionPathGroupStatsId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionCounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteractionCounts_InteractionStats_InteractionPathGroupStat~",
                        column: x => x.InteractionPathGroupStatsId,
                        principalTable: "InteractionStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteractionCounts_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WebSiteId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    FirstVisit = table.Column<bool>(nullable: false),
                    Referrer = table.Column<string>(nullable: true),
                    ScreenWidth = table.Column<double>(nullable: false),
                    ScreenHeight = table.Column<double>(nullable: false),
                    DevicePixelRatio = table.Column<double>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interactions_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interactions_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrowserStats_Browser",
                table: "BrowserStats",
                column: "Browser");

            migrationBuilder.CreateIndex(
                name: "IX_BrowserStats_Date",
                table: "BrowserStats",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_BrowserStats_WebSiteId",
                table: "BrowserStats",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_Date",
                table: "InteractionCounts",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_InteractionPathGroupStatsId",
                table: "InteractionCounts",
                column: "InteractionPathGroupStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_Path",
                table: "InteractionCounts",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionCounts_WebSiteId",
                table: "InteractionCounts",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_Browser",
                table: "Interactions",
                column: "Browser");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_CreatedAt",
                table: "Interactions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_DevicePixelRatio",
                table: "Interactions",
                column: "DevicePixelRatio");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_FirstVisit",
                table: "Interactions",
                column: "FirstVisit");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_Path",
                table: "Interactions",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_ScreenHeight",
                table: "Interactions",
                column: "ScreenHeight");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_ScreenWidth",
                table: "Interactions",
                column: "ScreenWidth");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_SessionId",
                table: "Interactions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_WebSiteId",
                table: "Interactions",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionStats_Date",
                table: "InteractionStats",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionStats_WebSiteId",
                table: "InteractionStats",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationStats_WebSiteId",
                table: "LocationStats",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PageViewStats_CreatedAt",
                table: "PageViewStats",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PageViewStats_WebSiteId",
                table: "PageViewStats",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSizeStats_Date",
                table: "ScreenSizeStats",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSizeStats_WebSiteId",
                table: "ScreenSizeStats",
                column: "WebSiteId");

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
                name: "IX_Sessions_WebSiteId",
                table: "Sessions",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemStats_Day",
                table: "SystemStats",
                column: "Day");

            migrationBuilder.CreateIndex(
                name: "IX_SystemStats_Platform",
                table: "SystemStats",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_SystemStats_WebSiteId",
                table: "SystemStats",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSites_APIKey",
                table: "WebSites",
                column: "APIKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebSites_OwnerId",
                table: "WebSites",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BrowserStats");

            migrationBuilder.DropTable(
                name: "InteractionCounts");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "LocationStats");

            migrationBuilder.DropTable(
                name: "PageViewStats");

            migrationBuilder.DropTable(
                name: "ScreenSizeStats");

            migrationBuilder.DropTable(
                name: "SystemStats");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "InteractionStats");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "WebSites");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
