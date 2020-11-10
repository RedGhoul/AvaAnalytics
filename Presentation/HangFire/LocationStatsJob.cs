using Domain;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.HangFire
{
    public class LocationStatsJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;

        public LocationStatsJob(ApplicationDbContext ctx)
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
            DateTime noww = DateTime.UtcNow;
            DateTime oneHourAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30));
            List<int> allSites = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
            for (int websiteIndex = 0; websiteIndex < allSites.Count; websiteIndex++)
            {
                List<LocationStats> locationCount = await _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= noww &&
                    x.CreatedAt > oneHourAgo)
                    .GroupBy(x => x.Location)
                    .Select(d => new LocationStats
                    {
                        Location = d.Key,
                        Count = d.Count()
                    }).ToListAsync();

                for (int locCountIndex = 0; locCountIndex < locationCount.Count; locCountIndex++)
                {
                    LocationStats InteractionCounts = new LocationStats
                    {
                        WebSiteId = allSites[websiteIndex],
                        Location = locationCount[locCountIndex].Location,
                        Date = DateTime.UtcNow,
                        Count = locationCount[locCountIndex].Count
                    };
                    _ctx.Add(InteractionCounts);
                    await _ctx.SaveChangesAsync();
                }
            }

        }
    }
}
