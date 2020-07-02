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
                var sizePhones = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= noww && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 384).Count();

                var sizeLargePhones = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= noww && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 1024 && x.ScreenWidth > 384)
                    .Count();

                var sizeTablets = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= noww && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 1440 && x.ScreenWidth > 1024)
                    .Count();

                var sizeDesktop = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= noww && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 1920 && x.ScreenWidth > 1440)
                    .Count();

                var sizeDesktopHD = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= noww && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth > 1920)
                    .Count();


                ScreenSizeStats screenSizeStats = new ScreenSizeStats
                {
                    NumberOfPhones = sizePhones,
                    LargePhonesSmallTablets = sizeLargePhones,
                    TabletsSmallLaptops = sizeTablets,
                    ComputerMonitors = sizeDesktop,
                    ComputerMonitors4K = sizeDesktopHD,
                    WebSiteId = allSites[websiteIndex],
                    Date = noww
                };

                _ctx.Add(screenSizeStats);
                await _ctx.SaveChangesAsync();
            }

        }
    }
}
