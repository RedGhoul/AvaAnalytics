using Hangfire;
using Microsoft.EntityFrameworkCore;
using SharpCounter.Data;
using SharpCounter.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.HangFire
{
    public class ScreenSizeStatsJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;

        public ScreenSizeStatsJob(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }


        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.Now);
        }

        public async Task RunAtTimeOf(DateTime now)
        {
            var noww = DateTime.UtcNow;
            var oneHourAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30));
            var allSites = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
            for (int websiteIndex = 0; websiteIndex < allSites.Count; websiteIndex++)
            {
                var pathCount = await _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= noww &&
                    x.CreatedAt > oneHourAgo)
                    .GroupBy(x => x.Location)
                    .Select(d => new LocationStats
                    {
                        Location = d.Key,
                        Count = d.Count()
                    }).ToListAsync();

                for (int pathCountIndex = 0; pathCountIndex < pathCount.Count; pathCountIndex++)
                {
                    LocationStats InteractionCounts = new LocationStats
                    {
                        WebSiteId = allSites[websiteIndex],
                        Location = pathCount[pathCountIndex].Location,
                        Date = DateTime.UtcNow,
                        Count = pathCount[pathCountIndex].Count
                    };
                    _ctx.Add(InteractionCounts);
                    await _ctx.SaveChangesAsync();
                }
            }

        }
    }
}
