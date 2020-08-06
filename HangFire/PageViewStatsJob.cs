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
    public class PageViewStatsJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;

        public PageViewStatsJob(ApplicationDbContext ctx)
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
            DateTime ThirtyMinsAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30));
            List<int> allSites = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
            for (int curSiteIndex = 0; curSiteIndex < allSites.Count; curSiteIndex++)
            {
                List<int> InteractionCount = await _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[curSiteIndex] &&
                    x.CreatedAt <= now &&
                    x.CreatedAt >= ThirtyMinsAgo)
                    .Select(x => x.Id).ToListAsync();

                PageViewStats pageViewStats = new PageViewStats()
                {
                    Count = InteractionCount.Count,
                    CreatedAt = now,
                    WebSiteId = allSites[curSiteIndex]
                };

                _ctx.Add(pageViewStats);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
