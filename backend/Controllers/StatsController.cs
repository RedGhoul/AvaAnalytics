using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpCounter.Dapper;
using SharpCounter.Data;
using SharpCounter.Enities;

namespace SharpCounter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly StatsRepo _statsRepo;
        public StatsController(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }

        // GET: api/Stats/BrowserStats/5
        [HttpGet("BrowserStats/{id}")]
        public async Task<List<BrowserStatsDTO>> GetBrowserStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromMinutes(30));
            var data = await _statsRepo.GetBrowserStats(curTime, oldTime, id);
            return data;
        }

        // GET: api/InteractionStats/5
        [HttpGet("InteractionStats/{id}")]
        public async Task<List<InteractionCountsDTO>> GetInteractionStats(int id)
        {
            var data = await _statsRepo.GetInteractionStats(id);
            return data;
        }

        // GET: api/Stats/SystemStats/5
        [HttpGet("SystemStats/{id}")]
        public async Task<List<SystemStatsDTO>> GetSystemStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromMinutes(30));
            var data = await _statsRepo.GetSystemStats(curTime, oldTime, id);
            return data;
        }

        // GET: api/Stats/ScreenSizeStats/5
        [HttpGet("ScreenSizeStats/{id}")]
        public async Task<List<ScreenSizeStatsDTO>> GetScreenSizeStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromMinutes(30));
            var data = await _statsRepo.GetScreenSizeStats(curTime, oldTime, id);
            return data;
        }

        [HttpGet("LocationStats/{id}")]
        public async Task<List<LocationStatsDTO>> GetLocationStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromMinutes(30));
            var data = await _statsRepo.GetLocationStats(curTime, oldTime, id);
            return data;
        }

        [HttpGet("PageViewCountStats/{id}")]
        public async Task<List<PageViewStatsDTO>> GetPageViewCountStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromDays(2));
            var data = await _statsRepo.GetPageViewCountStats(curTime, oldTime, id);
            return data;
        }
    }
}
