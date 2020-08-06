using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpCounter.Dapper;
using SharpCounter.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpCounter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatsController : ControllerBase
    {
        private readonly StatsRepo _statsRepo;
        public StatsController(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }

        // GET: api/Stats/BrowserStats/5
        [HttpPost("BrowserStats/{id}")]
        public async Task<List<BrowserStatsDTO>> GetBrowserStats(int id, DateRangeDTO dateRange)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentStartDate);
            List<BrowserStatsDTO> data = await _statsRepo.GetBrowserStats(curTime, oldTime, id);
            return data;
        }

        // GET: api/InteractionStats/5
        [HttpPost("InteractionStats/{id}")]
        public async Task<List<InteractionCountsDTO>> GetInteractionStats(int id, DateRangeDTO dateRange)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentStartDate);
            List<InteractionCountsDTO> data = await _statsRepo.GetInteractionStats(id);
            return data;
        }

        // GET: api/Stats/SystemStats/5
        [HttpPost("SystemStats/{id}")]
        public async Task<List<SystemStatsDTO>> GetSystemStats(int id, DateRangeDTO dateRange)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentStartDate);
            List<SystemStatsDTO> data = await _statsRepo.GetSystemStats(curTime, oldTime, id);
            return data;
        }

        // GET: api/Stats/ScreenSizeStats/5
        [HttpPost("ScreenSizeStats/{id}")]
        public async Task<List<ScreenSizeStatsDTO>> GetScreenSizeStats(int id, DateRangeDTO dateRange)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentStartDate); ;
            List<ScreenSizeStatsDTO> data = await _statsRepo.GetScreenSizeStats(curTime, oldTime, id);
            return data;
        }

        [HttpPost("LocationStats/{id}")]
        public async Task<List<LocationStatsDTO>> GetLocationStats(int id, DateRangeDTO dateRange)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentStartDate);
            List<LocationStatsDTO> data = await _statsRepo.GetLocationStats(curTime, oldTime, id);
            return data;
        }

        [HttpPost("PageViewCountStats/{id}")]
        public async Task<List<PageViewStatsDTO>> GetPageViewCountStats(int id, DateRangeDTO dateRange)
        {
            DateTime EndDate = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentEndDate);
            DateTime StartDate = TimeZoneInfo.ConvertTimeToUtc(dateRange.CurrentStartDate);
            List<PageViewStatsDTO> data = await _statsRepo.GetPageViewCountStats(StartDate, EndDate, id);
            return data;
        }
    }
}
