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
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class StatsRepo
    {
        private readonly ApplicationDbContext _context;

        public StatsRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrowserStatsDTO>> GetBrowserStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
           return await _context.BrowserStats.Where(
               x => x.Date <= curTime && 
               x.Date >= oldTime && 
               x.WebSiteId == webSiteId)
            .GroupBy(x => new { x.Browser, x.Version })
                .Select(x => new BrowserStatsDTO
                {
                    Browser = x.Key.Browser,
                    Version = x.Key.Version,
                    Count = x.Sum(c => c.Count)
                }).ToListAsync();
        }

        public async Task<List<InteractionByPathCountsDTO>> GetInteractionByPathCounts(DateTime curTime, DateTime oldTime, int webSiteId)
        {

            var InteractionPathGroupStatsIds = await _context.InteractionPathGroupStats
                .Where(x => x.WebSiteId == webSiteId && x.Date <= curTime && x.Date >= oldTime).Select(x => x.Id).ToListAsync();


            return await _context.InteractionByPathCounts
                .Where(x => x.WebSiteId == webSiteId && InteractionPathGroupStatsIds.Contains(x.InteractionPathGroupStatsId))
                .GroupBy(x => new { x.Path })
                .Select(x => new InteractionByPathCountsDTO { 
                    Path = x.Key.Path,
                    Total = x.Sum(c => c.Total)
                }).ToListAsync();
        }

        public async Task<List<SystemStatsDTO>> GetSystemStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            return await _context.SystemStats
                 .Where(x => x.WebSiteId == webSiteId && x.Day <= curTime && x.Day >= oldTime)
                 .GroupBy(x => new { x.Platform, x.Version })
                 .Select(x => new SystemStatsDTO
                 {
                     Platform = x.Key.Platform,
                     Version = x.Key.Version,
                     Count = x.Sum(c => c.Count)
                 }).ToListAsync();
        }

        public async Task<ScreenSizeStatsDTO> GetScreenSizeStatsAsync(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            var baseValues = _context.ScreenSizeStats.Where(x => x.WebSiteId == webSiteId && x.Date <= curTime && x.Date >= oldTime);
            return new ScreenSizeStatsDTO
            {
                NumberOfPhones = await baseValues.SumAsync(x => x.NumberOfPhones),
                LargePhonesSmallTablets = await baseValues.SumAsync(x => x.LargePhonesSmallTablets),
                TabletsSmallLaptops = await baseValues.SumAsync(x => x.TabletsSmallLaptops),
                ComputerMonitors = await baseValues.SumAsync(x => x.ComputerMonitors),
                ComputerMonitors4K = await baseValues.SumAsync(x => x.ComputerMonitors4K)
            };

        }

        public async Task<List<LocationStatsDTO>> GetLocationStats(DateTime curTime, DateTime oldTime, int webSiteId)
        {
            return await _context.LocationStats
                 .Where(x => x.WebSiteId == webSiteId && x.Date <= curTime && x.Date >= oldTime)
                 .GroupBy(x => new { x.Location })
                 .Select(x => new LocationStatsDTO
                 {
                     Location = x.Key.Location,
                     Count = x.Sum(c => c.Count),
                 }).ToListAsync();
        }

        public async Task<List<PageViewStatsDTO>> GetPageViewCountStats(DateTime curTime, DateTime oldTime, string timeZoneName, int webSiteId)
        {
            IEnumerable<PageViewStatsDTO> data = await _context.PageViewStats
               .Where(x => x.WebSiteId == webSiteId 
                    && x.CreatedAt <= curTime && x.CreatedAt >= oldTime && x.Count > 0)
               .OrderByDescending(x => x.CreatedAt)
               .Select(x => new PageViewStatsDTO
               {
                   Count = x.Count,
                   CreatedAt = x.CreatedAt
               }).ToListAsync();

            List<PageViewStatsDTO> listOfDtos = DateTimeDTOHelper.SetTimeZone(data, timeZoneName);
            return data.ToList();
        }

        public async Task<List<PageViewStatsDTO>> GetNonZeroPageViewCountStats(DateTime curTime, DateTime oldTime, string timeZoneName, int webSiteId)
        {
            IEnumerable<PageViewStatsDTO> data = await _context.PageViewStats
               .Where(x => x.WebSiteId == webSiteId
                    && x.CreatedAt <= curTime && x.CreatedAt >= oldTime && x.Count > 0)
               .OrderByDescending(x => x.CreatedAt)
               .Select(x => new PageViewStatsDTO
               {
                   Count = x.Count,
                   CreatedAt = x.CreatedAt
               }).Take(5).ToListAsync();

            List<PageViewStatsDTO> listOfDtos = DateTimeDTOHelper.SetTimeZone(data, timeZoneName);
            return listOfDtos;
        }


    }
}
