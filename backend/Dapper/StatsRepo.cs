using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SharpCounter.Data;
using SharpCounter.Enities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Dapper
{
    public class StatsRepo 
    {
        private readonly string connectionString;
        public StatsRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public async Task<List<BrowserStatsDTO>> GetBrowserStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            var data = await dbConnection.QueryAsync<BrowserStatsDTO>(
                @"SELECT ""Browser"", ""Version"", SUM(""Count"") as ""Count"" FROM ""BrowserStats"" where  
                ""Date"" <= @curTime and ""Date"" >= @oldTime and ""WebSiteId"" = @Id GROUP By ""Browser"", ""Version""",
                new { curTime, oldTime , Id = webSiteId });
            return data.ToList();
        }

        public async Task<List<InteractionCountsDTO>> GetInteractionStats(int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            var data = await dbConnection.QueryAsync<InteractionCountsDTO>(
                @"SELECT ""Path"", ""Date"", ""Total"" FROM ""InteractionCounts"" where ""InteractionStatsId"" = (
                SELECT IDDATE.""Id"" from(SELECT ""Id"", ""Date"" FROM ""InteractionStats"" WHERE ""WebSiteId"" = 2)
                as IDDATE where IDDATE.""Date"" = (SELECT MAX(IDDATE2.""Date"") FROM(SELECT ""Id"", ""Date"" FROM 
                ""InteractionStats"" WHERE ""WebSiteId"" = 2) as IDDATE2)) and ""WebSiteId"" = 2;",
                  new { Id = webSiteId });
            return data.ToList();
        }

        public async Task<List<SystemStatsDTO>> GetSystemStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            var data = await dbConnection.QueryAsync<SystemStatsDTO>(
                @"SELECT ""Platform"", ""Version"", SUM(""Count"") as ""Count"" 
                FROM ""SystemStats"" where ""Day"" <= @curTime and 
                ""Day"" >= @oldTime and ""WebSiteId"" = @Id 
                GROUP By ""Platform"", ""Version""",
                new { curTime, oldTime, Id = webSiteId });
            return data.ToList();
        }

        public async Task<List<ScreenSizeStatsDTO>> GetScreenSizeStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            var data = await dbConnection.QueryAsync<ScreenSizeStatsDTO>(
                @"SELECT SUM(""NumberOfPhones"") as ""NumberOfPhones"", SUM(""LargePhonesSmallTablets"") as ""LargePhonesSmallTablets"",
                SUM(""TabletsSmallLaptops"") as ""TabletsSmallLaptops"",SUM(""ComputerMonitors"") ""ComputerMonitors"",
                SUM(""ComputerMonitors4K"") as ""ComputerMonitors4K"" FROM ""ScreenSizeStats"" where ""Date"" <= @curTime and 
                ""Date"" >= @oldTime and ""WebSiteId"" = @Id",
                new { curTime, oldTime, Id = webSiteId });
            return data.ToList();
        }

        public async Task<List<LocationStatsDTO>> GetLocationStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            var data = await dbConnection.QueryAsync<LocationStatsDTO>(
                @"SELECT ""Location"", SUM(""Count"")
                FROM ""LocationStats"" where ""Date"" <= @curTime and 
                ""Date"" >= @oldTime and ""WebSiteId"" = @Id group by ""Location""",
                new { curTime, oldTime, Id = webSiteId });
            return data.ToList();
        }
    }
}
