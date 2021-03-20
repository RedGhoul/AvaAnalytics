using Application.DTO;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Persistence;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class StatsRepo
    {
        private readonly string connectionString;
        internal IDbConnection Connection => new NpgsqlConnection(connectionString);
        public StatsRepo(IConfiguration configuration)
        {
            connectionString = Application.AppSecrets.GetConnectionString(configuration);
        }

      
        public async Task<List<BrowserStatsDTO>> GetBrowserStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            IEnumerable<BrowserStatsDTO> data = await dbConnection.QueryAsync<BrowserStatsDTO>(
                @"SELECT ""Browser"", ""Version"", SUM(""Count"") as Count FROM ""BrowserStats"" where  
                ""Date"" <= @curTime and ""Date"" >= @oldTime and ""WebSiteId"" = @Id GROUP By ""Browser"", ""Version""",
                new { curTime, oldTime, Id = webSiteId });
            return data.ToList();
        }

        public async Task<List<InteractionByPathCountsDTO>> GetInteractionByPathCounts(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            IEnumerable<InteractionByPathCountsDTO> data = await dbConnection.QueryAsync<InteractionByPathCountsDTO>(
                @"SELECT ""Path"", SUM(""Total"") as Total FROM ""InteractionByPathCounts"" where 
                ""InteractionPathGroupStatsId"" in ( SELECT ""Id"" FROM ""InteractionPathGroupStats"" 
                WHERE ""WebSiteId"" = @Id and ""Date"" <= @curTime and ""Date"" >= @oldTime) 
                and ""WebSiteId"" = @Id group by ""Path""",
                  new { curTime, oldTime, Id = webSiteId });
            return data.ToList();
        }
        public async Task<List<SystemStatsDTO>> GetSystemStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            IEnumerable<SystemStatsDTO> data = await dbConnection.QueryAsync<SystemStatsDTO>(
                @"SELECT ""Platform"", ""Version"", SUM(""Count"") as Count 
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
            IEnumerable<ScreenSizeStatsDTO> data = await dbConnection.QueryAsync<ScreenSizeStatsDTO>(
                @"SELECT SUM(""NumberOfPhones"") as NumberOfPhones, SUM(""LargePhonesSmallTablets"") as LargePhonesSmallTablets,
                SUM(""TabletsSmallLaptops"") as TabletsSmallLaptops,SUM(""ComputerMonitors"") ComputerMonitors,
                SUM(""ComputerMonitors4K"") as ComputerMonitors4K FROM ""ScreenSizeStats"" where ""Date"" <= @curTime and 
                ""Date"" >= @oldTime and ""WebSiteId"" = @Id",
                new { curTime, oldTime, Id = webSiteId });
            return data.ToList();
        }

        public async Task<List<LocationStatsDTO>> GetLocationStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            IEnumerable<LocationStatsDTO> data = await dbConnection.QueryAsync<LocationStatsDTO>(
                @"SELECT ""Location"", SUM(""Count"") as Count
                FROM ""LocationStats"" where ""Date"" <= @curTime and 
                ""Date"" >= @oldTime and ""WebSiteId"" = @Id group by ""Location""",
                new { curTime, oldTime, Id = webSiteId });

            return data.ToList();
        }

        public async Task<List<PageViewStatsDTO>> GetPageViewCountStats(DateTime curTime, DateTime oldTime, string timeZoneName, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            IEnumerable<PageViewStatsDTO> data = await dbConnection.QueryAsync<PageViewStatsDTO>(
                @"SELECT ""Count"", ""CreatedAt"" FROM ""PageViewStats"" where ""CreatedAt"" <= @curTime and 
                ""CreatedAt"" >= @oldTime and ""WebSiteId"" = @Id order by ""CreatedAt"" ASC",
                new { curTime, oldTime, Id = webSiteId });

            List<PageViewStatsDTO> listOfDtos = DateTimeDTOHelper.SetTimeZone(data, timeZoneName);
            return data.ToList();
        }

        public async Task<List<PageViewStatsDTO>> GetNonZeroPageViewCountStats(DateTime curTime, DateTime oldTime, string timeZoneName, int webSiteId)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            IEnumerable<PageViewStatsDTO> data = await dbConnection.QueryAsync<PageViewStatsDTO>(
                @"SELECT ""Count"", ""CreatedAt"" FROM ""PageViewStats"" where ""CreatedAt"" <= @curTime and 
                ""CreatedAt"" >= @oldTime and ""WebSiteId"" = @Id and ""Count"" != 0 order by ""CreatedAt"" DESC LIMIT 5",
                new { curTime, oldTime, Id = webSiteId });

            List<PageViewStatsDTO> listOfDtos = DateTimeDTOHelper.SetTimeZone(data, timeZoneName);
            return listOfDtos;
        }

    }
}
