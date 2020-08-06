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
    public class InteractionByPathStatsJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;

        public InteractionByPathStatsJob(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.UtcNow);
        }

        public async Task RunAtTimeOf(DateTime now)
        {
            DateTime oneHourAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30));
            List<int> allSites = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
            for (int websiteIndex = 0; websiteIndex < allSites.Count; websiteIndex++)
            {
                List<InteractionByPathCounts> pathCount = await _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= now &&
                    x.CreatedAt > oneHourAgo)
                    .GroupBy(x => x.Path)
                    .Select(d => new InteractionByPathCounts
                    {
                        Path = d.Key,
                        Total = d.Count()
                    }).ToListAsync();

                InteractionPathGroupStats interactionPathGroupStats = new InteractionPathGroupStats
                {
                    Date = DateTime.UtcNow,
                    WebSiteId = allSites[websiteIndex],
                    TotalRoutes = pathCount.Count
                };
                _ctx.Add(interactionPathGroupStats);
                await _ctx.SaveChangesAsync();

                for (int pathCountIndex = 0; pathCountIndex < pathCount.Count; pathCountIndex++)
                {
                    InteractionByPathCounts InteractionByPathCounts = new InteractionByPathCounts
                    {
                        WebSiteId = allSites[websiteIndex],
                        Path = pathCount[pathCountIndex].Path,
                        Date = DateTime.UtcNow,
                        Total = pathCount[pathCountIndex].Total,
                        InteractionPathGroupStatsId = interactionPathGroupStats.Id
                    };
                    _ctx.Add(InteractionByPathCounts);
                    await _ctx.SaveChangesAsync();
                }


            }

        }
    }
}
