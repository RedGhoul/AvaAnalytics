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
    public class BrowserStatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly BrowserStatsRepo _browserStatsRepo;
        public BrowserStatsController(BrowserStatsRepo browserStatsRepo, ApplicationDbContext context)
        {
            _context = context;
            _browserStatsRepo = browserStatsRepo;
        }

        // GET: api/BrowserStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrowserStatDTO>>> GetBrowserStats()
        {
            var curTime = DateTime.UtcNow;
            var oldTime = curTime.Subtract(TimeSpan.FromDays(5));
            var data = await _browserStatsRepo.GetBrowserStats(curTime, oldTime);
            return data.ToList();
        }

        // GET: api/BrowserStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrowserStats>> GetBrowserStats(int id)
        {
            var browserStats = await _context.BrowserStats.FindAsync(id);

            if (browserStats == null)
            {
                return NotFound();
            }

            return browserStats;
        }

        // PUT: api/BrowserStats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrowserStats(int id, BrowserStats browserStats)
        {
            if (id != browserStats.Id)
            {
                return BadRequest();
            }

            _context.Entry(browserStats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrowserStatsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BrowserStats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BrowserStats>> PostBrowserStats(BrowserStats browserStats)
        {
            _context.BrowserStats.Add(browserStats);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrowserStats", new { id = browserStats.Id }, browserStats);
        }

        // DELETE: api/BrowserStats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BrowserStats>> DeleteBrowserStats(int id)
        {
            var browserStats = await _context.BrowserStats.FindAsync(id);
            if (browserStats == null)
            {
                return NotFound();
            }

            _context.BrowserStats.Remove(browserStats);
            await _context.SaveChangesAsync();

            return browserStats;
        }

        private bool BrowserStatsExists(int id)
        {
            return _context.BrowserStats.Any(e => e.Id == id);
        }
    }
}
