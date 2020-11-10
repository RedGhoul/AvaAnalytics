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
            await RunAtTimeOf(DateTime.UtcNow);
        }

        public async Task RunAtTimeOf(DateTime now)
        {
            DateTime oneHourAgo = now.Subtract(TimeSpan.FromMinutes(30));
            List<int> allSites = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
            for (int websiteIndex = 0; websiteIndex < allSites.Count; websiteIndex++)
            {
                int sizePhones = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= now && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 384).Count();

                int sizeLargePhones = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= now && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 1024 && x.ScreenWidth > 384)
                    .Count();

                int sizeTablets = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= now && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 1440 && x.ScreenWidth > 1024)
                    .Count();

                int sizeDesktop = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= now && x.CreatedAt > oneHourAgo &&
                    x.ScreenWidth != 0 && x.ScreenWidth <= 1920 && x.ScreenWidth > 1440)
                    .Count();

                int sizeDesktopHD = _ctx.Interactions.Where(
                    x => x.WebSiteId == allSites[websiteIndex] &&
                    x.CreatedAt <= now && x.CreatedAt > oneHourAgo &&
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
                    Date = now
                };

                _ctx.Add(screenSizeStats);
                await _ctx.SaveChangesAsync();
            }

        }
    }
}
