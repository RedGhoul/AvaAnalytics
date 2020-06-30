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

        public async Task<IEnumerable<BrowserStatsDTO>> GetBrowserStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return await dbConnection.QueryAsync<BrowserStatsDTO>(
                @"SELECT ""Browser"", ""Version"", SUM(""Count"") as ""Count"" FROM ""BrowserStats"" where  
                ""Date"" <= @curTime and ""Date"" >= @oldTime and ""WebSiteId"" = @Id GROUP By ""Browser"", ""Version""",
                new { curTime, oldTime , Id = webSiteId });
        }

        public async Task<IEnumerable<InteractionCountsDTO>> GetInteractionStats(int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return await dbConnection.QueryAsync<InteractionCountsDTO>(
                @"SELECT ""Path"", ""Date"", ""Total"" FROM ""InteractionCounts"" where ""InteractionStatsId"" = (
                  SELECT ""Id"" FROM ""InteractionStats"" WHERE ""Date"" = (
                  SELECT MAX(""Date"") FROM  ""InteractionStats"")) and ""WebSiteId"" = @Id",
                  new { Id = webSiteId });
        }

        public async Task<IEnumerable<SystemStatsDTO>> GetSystemStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return await dbConnection.QueryAsync<SystemStatsDTO>(
                @"SELECT ""Platform"", ""Version"", SUM(""Count"") as ""Count"" 
                FROM ""SystemStats"" where ""Day"" <= @curTime and 
                ""Day"" >= @oldTime and ""WebSiteId"" = @Id 
                GROUP By ""Platform"", ""Version""",
                new { curTime, oldTime, Id = webSiteId });
        }

        public async Task<IEnumerable<SystemStatsDTO>> GetScreenSizeStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return await dbConnection.QueryAsync<SystemStatsDTO>(
                @"SELECT ""Platform"", ""Version"", SUM(""Count"") as ""Count"" 
                FROM ""SystemStats"" where ""Day"" <= @curTime and 
                ""Day"" >= @oldTime and ""WebSiteId"" = @Id 
                GROUP By ""Platform"", ""Version""",
                new { curTime, oldTime, Id = webSiteId });
        }
    }
}
