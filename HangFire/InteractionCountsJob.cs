using Dapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using SharpCounter.Data;
using SharpCounter.Enities;
using SharpCounter.HangFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace SharpCounter.HangFire
{
    public class InteractionCountsJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;

        public InteractionCountsJob(ApplicationDbContext ctx)
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
                    .GroupBy(x => x.Path)
                    .Select(d => new InteractionCounts
                    {
                        Path = d.Key,
                        Total = d.Count()
                    }).ToListAsync();

                for (int pathCountIndex = 0; pathCountIndex < pathCount.Count; pathCountIndex++)
                {
                    InteractionCounts InteractionCounts = new InteractionCounts
                    {
                        WebSiteId = allSites[websiteIndex],
                        Path = pathCount[pathCountIndex].Path,
                        Hour = DateTime.UtcNow,
                        Total = pathCount[pathCountIndex].Total
                    };
                    _ctx.Add(InteractionCounts);
                    await _ctx.SaveChangesAsync();
                }
            }

        }
    }
}
