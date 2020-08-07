using Domain;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Persistence;
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
            DateTime noww = DateTime.UtcNow;
            DateTime oneHourAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30));
            List<int> allSiteIds = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
            for (int SiteIdIndex = 0; SiteIdIndex < allSiteIds.Count; SiteIdIndex++)
            {
                List<SystemStats> browserInfoFromInteractionArgs = await _ctx.Interactions.Where(
                    x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
                    x.CreatedAt <= noww &&
                    x.CreatedAt > oneHourAgo)
                    .GroupBy(x => x.Browser)
                    .Select(d => new SystemStats
                    {
                        Version = d.Key,
                        Count = d.Count()
                    }).ToListAsync();

                for (int ArgsIndex = 0; ArgsIndex < browserInfoFromInteractionArgs.Count; ArgsIndex++)
                {
                    Parser uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(browserInfoFromInteractionArgs[ArgsIndex].Version);
                    SystemStats SystemStats = new SystemStats
                    {
                        Platform = c.OS.Family,
                        Version = c.OS.Major,
                        Count = browserInfoFromInteractionArgs[ArgsIndex].Count,
                        Day = DateTime.UtcNow,
                        WebSiteId = allSiteIds[SiteIdIndex]
                    };
                    _ctx.Add(SystemStats);
                    await _ctx.SaveChangesAsync();
                }
            }


        }
    }
}
