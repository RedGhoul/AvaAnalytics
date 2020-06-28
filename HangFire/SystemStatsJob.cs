using Hangfire;
using Microsoft.EntityFrameworkCore;
using SharpCounter.Data;
using SharpCounter.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAParser;

namespace SharpCounter.HangFire
{
    public class SystemStatsJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;

        public SystemStatsJob(ApplicationDbContext ctx)
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
            for (int i = 0; i < allSites.Count; i++)
            {
                var getFirstTime = await _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[i] &&
                    x.CreatedAt <= noww &&
                    x.CreatedAt > oneHourAgo)
                    .GroupBy(x => x.Browser)
                    .Select(d => new SystemStats
                    {
                        Version = d.Key,
                        Count = d.Count()
                    }).ToListAsync();

                for (int j = 0; j < getFirstTime.Count; j++)
                {
                    var uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(getFirstTime[i].Version);
                    SystemStats SystemStats = new SystemStats
                    {
                        Platform = c.OS.Family,
                        Version = c.OS.Major,
                        Count = getFirstTime[i].Count,
                        Day = DateTime.UtcNow,
                        WebSiteId = allSites[i]
                    };
                    _ctx.Add(SystemStats);
                    await _ctx.SaveChangesAsync();
                }
            }


        }
    }
}
