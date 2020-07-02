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
        public async Task<ActionResult<IEnumerable<BrowserStatsDTO>>> GetBrowserStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromMinutes(30));
            var data = await _statsRepo.GetBrowserStats(curTime, oldTime, id);
            return data.ToList();
        }

        // GET: api/InteractionStats/5
        [HttpGet("InteractionStats/{id}")]
        public async Task<ActionResult<IEnumerable<InteractionCountsDTO>>> GetInteractionStats(int id)
        {
            var data = await _statsRepo.GetInteractionStats(id);
            return data.ToList();
        }

        // GET: api/Stats/SystemStats/5
        [HttpGet("SystemStats/{id}")]
        public async Task<ActionResult<IEnumerable<SystemStatsDTO>>> GetSystemStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromMinutes(30));
            var data = await _statsRepo.GetSystemStats(curTime, oldTime, id);
            return data.ToList();
        }

        // GET: api/Stats/ScreenSizeStats/5
        [HttpGet("ScreenSizeStats/{id}")]
        public async Task<ActionResult<IEnumerable<ScreenSizeStatsDTO>>> GetScreenSizeStats(int id)
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromMinutes(30));
            var data = await _statsRepo.GetScreenSizeStats(curTime, oldTime, id);
            return data.ToList();
        }

    }
}
