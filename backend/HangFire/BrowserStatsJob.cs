using Hangfire;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public class BrowserStatsJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;

        public BrowserStatsJob(ApplicationDbContext ctx)
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
                    .Select(d => new BrowserStats
                    {
                        Browser = d.Key,
                        Count = d.Count()
                    }).ToListAsync();
                
                for (int j = 0; j < getFirstTime.Count; j++)
                {
                    var uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(getFirstTime[j].Browser);
                    BrowserStats browserStats = new BrowserStats
                    {
                        Browser = c.UA.Family,
                        Version = c.UA.Major,
                        Count = getFirstTime[j].Count,
                        Date = DateTime.UtcNow,
                        WebSiteId = allSites[i]
                    };
                    _ctx.Add(browserStats);
                    await _ctx.SaveChangesAsync();
                }
            }
        }
    }
}
