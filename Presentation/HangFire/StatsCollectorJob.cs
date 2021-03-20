using Application;
using Domain;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAParser;

namespace Presentation.HangFire
{
    public class StatsCollectorJob : IMyJob
    {
        private readonly ApplicationDbContext _ctx;
        private readonly string _DbConnectionString;

        public StatsCollectorJob(ApplicationDbContext ctx, IConfiguration config)
        {
            _ctx = ctx;
            _DbConnectionString = AppSecrets.GetConnectionString(config);
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf(DateTime.Now);
        }

        public async Task RunAtTimeOf(DateTime currentTime)
        {
            DateTime fiveMinsAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(5));

            List<int> allSites = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseNpgsql(_DbConnectionString);

            for (int websiteIndex = 0; websiteIndex < allSites.Count; websiteIndex++)
            {
                using (var _ctx = new ApplicationDbContext(optionsBuilder.Options))
                {
                    List<LocationStats> locationCount = await _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime &&
                        x.CreatedAt > fiveMinsAgo)
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
                            Date = currentTime,
                            Count = locationCount[locCountIndex].Count
                        };
                        _ctx.Add(InteractionCounts);
                        await _ctx.SaveChangesAsync();
                    }

                    List<BrowserStats> getFirstTime = await _ctx.Interactions.Where(
                       x => x.WebSiteId == allSites[websiteIndex] &&
                       x.CreatedAt <= currentTime &&
                       x.CreatedAt > fiveMinsAgo)
                       .GroupBy(x => x.Browser)
                       .Select(d => new BrowserStats
                       {
                           Browser = d.Key,
                           Count = d.Count()
                       }).ToListAsync();

                    for (int j = 0; j < getFirstTime.Count; j++)
                    {
                        Parser uaParser = Parser.GetDefault();
                        ClientInfo c = uaParser.Parse(getFirstTime[j].Browser);
                        BrowserStats browserStats = new BrowserStats
                        {
                            Browser = c.UA.Family,
                            Version = c.UA.Major,
                            Count = getFirstTime[j].Count,
                            Date = currentTime,
                            WebSiteId = allSites[websiteIndex]
                        };
                        _ctx.Add(browserStats);
                        await _ctx.SaveChangesAsync();
                    }

                    List<InteractionByPathCounts> pathCount = await _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime &&
                        x.CreatedAt > fiveMinsAgo)
                        .GroupBy(x => x.Path)
                        .Select(d => new InteractionByPathCounts
                        {
                            Path = d.Key,
                            Total = d.Count()
                        }).ToListAsync();

                    InteractionPathGroupStats interactionPathGroupStats = new InteractionPathGroupStats
                    {
                        Date = currentTime,
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
                            Date = currentTime,
                            Total = pathCount[pathCountIndex].Total,
                            InteractionPathGroupStatsId = interactionPathGroupStats.Id
                        };
                        _ctx.Add(InteractionByPathCounts);
                        await _ctx.SaveChangesAsync();
                    }

                    for (int curSiteIndex = 0; curSiteIndex < allSites.Count; curSiteIndex++)
                    {
                        List<int> InteractionCount = await _ctx.Interactions.Where(
                            x => x.WebSiteId == allSites[curSiteIndex] &&
                            x.CreatedAt <= currentTime &&
                            x.CreatedAt >= fiveMinsAgo)
                            .Select(x => x.Id).ToListAsync();

                        PageViewStats pageViewStats = new PageViewStats()
                        {
                            Count = InteractionCount.Count,
                            CreatedAt = currentTime,
                            WebSiteId = allSites[curSiteIndex]
                        };

                        _ctx.Add(pageViewStats);
                        await _ctx.SaveChangesAsync();
                    }


                    int sizePhones = _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime && x.CreatedAt > fiveMinsAgo &&
                        x.ScreenWidth != 0 && x.ScreenWidth <= 384).Count();

                    int sizeLargePhones = _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime && x.CreatedAt > fiveMinsAgo &&
                        x.ScreenWidth != 0 && x.ScreenWidth <= 1024 && x.ScreenWidth > 384)
                        .Count();

                    int sizeTablets = _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime && x.CreatedAt > fiveMinsAgo &&
                        x.ScreenWidth != 0 && x.ScreenWidth <= 1440 && x.ScreenWidth > 1024)
                        .Count();

                    int sizeDesktop = _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime && x.CreatedAt > fiveMinsAgo &&
                        x.ScreenWidth != 0 && x.ScreenWidth <= 1920 && x.ScreenWidth > 1440)
                        .Count();

                    int sizeDesktopHD = _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime && x.CreatedAt > fiveMinsAgo &&
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
                        Date = currentTime
                    };

                    _ctx.Add(screenSizeStats);
                    await _ctx.SaveChangesAsync();


                    List<SystemStats> browserInfoFromInteractionArgs = await _ctx.Interactions.Where(
                        x => x.WebSiteId == allSites[websiteIndex] &&
                        x.CreatedAt <= currentTime &&
                        x.CreatedAt > fiveMinsAgo)
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
                            Day = currentTime,
                            WebSiteId = allSites[websiteIndex]
                        };
                        _ctx.Add(SystemStats);
                        await _ctx.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
