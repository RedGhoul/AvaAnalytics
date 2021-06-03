using Microsoft.EntityFrameworkCore.Migrations;

namespace Presentation.Migrations
{
    public partial class create_GetBrowserStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: @"
                    SET ANSI_NULLS ON
                    GO
                    SET QUOTED_IDENTIFIER ON
                    GO
                    -- =============================================
                    -- Author:		Ava
                    -- Create date: <Create Date,,>
                    -- Description:	<Description,,>
                    -- =============================================
                    CREATE PROCEDURE GetBrowserStats
	                    -- Add the parameters for the stored procedure here
	                    @curTime DATETIME = NULL, 
	                    @oldTime DATETIME = NULL, 
	                    @webSiteId INT = 0
                    AS
                    BEGIN
	                    SELECT Browser, Version, SUM(Count) as Count FROM BrowserStats where  
                            Date <= @curTime and Date >= @oldTime and WebSiteId = @webSiteId GROUP By Browser, Version
                    END
                    GO

                ",
                suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               sql: "DROP PROCEDURE GetBrowserStats",
               suppressTransaction: true);
        }
    }
}
