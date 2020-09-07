using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using UAParser;

namespace DataAggregator
{
    public class DataAggregator
    {

        [FunctionName("DataAggregator")]
        public async void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {

            string connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            using (var _ctx = new ApplicationDbContext(optionsBuilder.Options))
            {
                DateTime now = DateTime.UtcNow;
                DateTime ThirtyMinAgo = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(30));
                List<int> allSiteIds = await _ctx.WebSites.Select(x => x.Id).ToListAsync();
                for (int SiteIdIndex = 0; SiteIdIndex < allSiteIds.Count; SiteIdIndex++)
                {
                    /**
                     SystemStatsJob
                     */
                    await StartSystemsStartJob(log, _ctx, now, ThirtyMinAgo, allSiteIds, SiteIdIndex);

                    /**
                     ScreenSizeStatsJob
                     */
                    await StartScreenSizeStatesJob(log, _ctx, now, ThirtyMinAgo, allSiteIds, SiteIdIndex);

                    /**
                     PageViewStatsJob
                     */
                    await StartPageViewStatsJob(log, _ctx, now, ThirtyMinAgo, allSiteIds, SiteIdIndex);

                    /*
                     LocationStatsJob
                     */

                    await StartLocationStatsJob(log, _ctx, now, ThirtyMinAgo, allSiteIds, SiteIdIndex);

                    /*
                     InteractionByPathStatsJob
                     */
                    await StartInteractionByPathStatsJob(log, _ctx, now, ThirtyMinAgo, allSiteIds, SiteIdIndex);

                    /*
                     BrowserStatsJob
                     */

                    await StartBrowserStatsJob(log, _ctx, now, ThirtyMinAgo, allSiteIds, SiteIdIndex);

                }
            }

            
        }

        private static async Task StartBrowserStatsJob(ILogger log, ApplicationDbContext _ctx, DateTime now, DateTime ThirtyMinAgo, List<int> allSiteIds, int SiteIdIndex)
        {
            log.LogInformation("Started BrowserStatsJob");

            List<BrowserStats> getFirstTime = await _ctx.Interactions.Where(
            x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
            x.CreatedAt <= now &&
            x.CreatedAt > ThirtyMinAgo)
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
                    Date = DateTime.UtcNow,
                    WebSiteId = allSiteIds[SiteIdIndex]
                };
                _ctx.Add(browserStats);
                await _ctx.SaveChangesAsync();
            }
            log.LogInformation("Finished BrowserStatsJob");
        }

        private static async Task StartInteractionByPathStatsJob(ILogger log, ApplicationDbContext _ctx, DateTime now, DateTime ThirtyMinAgo, List<int> allSiteIds, int SiteIdIndex)
        {
            log.LogInformation("Started InteractionByPathStatsJob");

            List<InteractionByPathCounts> pathCount = await _ctx.Interactions.Where(
            x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
            x.CreatedAt <= now &&
            x.CreatedAt > ThirtyMinAgo)
            .GroupBy(x => x.Path)
            .Select(d => new InteractionByPathCounts
            {
                Path = d.Key,
                Total = d.Count()
            }).ToListAsync();

            InteractionPathGroupStats interactionPathGroupStats = new InteractionPathGroupStats
            {
                Date = DateTime.UtcNow,
                WebSiteId = allSiteIds[SiteIdIndex],
                TotalRoutes = pathCount.Count
            };
            _ctx.Add(interactionPathGroupStats);
            await _ctx.SaveChangesAsync();

            for (int pathCountIndex = 0; pathCountIndex < pathCount.Count; pathCountIndex++)
            {
                InteractionByPathCounts InteractionByPathCounts = new InteractionByPathCounts
                {
                    WebSiteId = allSiteIds[SiteIdIndex],
                    Path = pathCount[pathCountIndex].Path,
                    Date = DateTime.UtcNow,
                    Total = pathCount[pathCountIndex].Total,
                    InteractionPathGroupStatsId = interactionPathGroupStats.Id
                };
                _ctx.Add(InteractionByPathCounts);
                await _ctx.SaveChangesAsync();
            }
            log.LogInformation("Finished InteractionByPathStatsJob");
        }

        private static async Task StartLocationStatsJob(ILogger log, ApplicationDbContext _ctx, DateTime now, DateTime ThirtyMinAgo, List<int> allSiteIds, int SiteIdIndex)
        {
            log.LogInformation("Started LocationStatsJob");

            List<LocationStats> locationCount = await _ctx.Interactions.Where(
            x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
            x.CreatedAt <= now &&
            x.CreatedAt > ThirtyMinAgo)
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
                    WebSiteId = allSiteIds[SiteIdIndex],
                    Location = locationCount[locCountIndex].Location,
                    Date = DateTime.UtcNow,
                    Count = locationCount[locCountIndex].Count
                };
                _ctx.Add(InteractionCounts);
                await _ctx.SaveChangesAsync();
            }

            log.LogInformation("Finished LocationStatsJob");
        }

        private static async Task StartPageViewStatsJob(ILogger log, ApplicationDbContext _ctx, DateTime now, DateTime ThirtyMinAgo, List<int> allSiteIds, int SiteIdIndex)
        {
            log.LogInformation("Started PageViewStatsJob");

            List<int> InteractionCount = await _ctx.Interactions.Where(
            x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
            x.CreatedAt <= now &&
            x.CreatedAt >= ThirtyMinAgo)
            .Select(x => x.Id).ToListAsync();

            PageViewStats pageViewStats = new PageViewStats()
            {
                Count = InteractionCount.Count,
                CreatedAt = now,
                WebSiteId = allSiteIds[SiteIdIndex]
            };

            _ctx.Add(pageViewStats);
            await _ctx.SaveChangesAsync();

            log.LogInformation("Finised PageViewStatsJob");
        }

        private static async Task StartScreenSizeStatesJob(ILogger log, ApplicationDbContext _ctx, DateTime now, DateTime ThirtyMinAgo, List<int> allSiteIds, int SiteIdIndex)
        {
            log.LogInformation("Started ScreenSizeStatsJob");
            int sizePhones = _ctx.Interactions.Where(
                                        x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
                                        x.CreatedAt <= now && x.CreatedAt > ThirtyMinAgo &&
                                        x.ScreenWidth != 0 && x.ScreenWidth <= 384).Count();

            int sizeLargePhones = _ctx.Interactions.Where(
                x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
                x.CreatedAt <= now && x.CreatedAt > ThirtyMinAgo &&
                x.ScreenWidth != 0 && x.ScreenWidth <= 1024 && x.ScreenWidth > 384)
                .Count();

            int sizeTablets = _ctx.Interactions.Where(
                x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
                x.CreatedAt <= now && x.CreatedAt > ThirtyMinAgo &&
                x.ScreenWidth != 0 && x.ScreenWidth <= 1440 && x.ScreenWidth > 1024)
                .Count();

            int sizeDesktop = _ctx.Interactions.Where(
                x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
                x.CreatedAt <= now && x.CreatedAt > ThirtyMinAgo &&
                x.ScreenWidth != 0 && x.ScreenWidth <= 1920 && x.ScreenWidth > 1440)
                .Count();

            int sizeDesktopHD = _ctx.Interactions.Where(
                x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
                x.CreatedAt <= now && x.CreatedAt > ThirtyMinAgo &&
                x.ScreenWidth != 0 && x.ScreenWidth > 1920)
                .Count();


            ScreenSizeStats screenSizeStats = new ScreenSizeStats
            {
                NumberOfPhones = sizePhones,
                LargePhonesSmallTablets = sizeLargePhones,
                TabletsSmallLaptops = sizeTablets,
                ComputerMonitors = sizeDesktop,
                ComputerMonitors4K = sizeDesktopHD,
                WebSiteId = allSiteIds[SiteIdIndex],
                Date = now
            };

            _ctx.Add(screenSizeStats);
            await _ctx.SaveChangesAsync();

            log.LogInformation("Finished ScreenSizeStatsJob");
        }

        private static async Task StartSystemsStartJob(ILogger log, ApplicationDbContext _ctx, DateTime now, DateTime ThirtyMinAgo, List<int> allSiteIds, int SiteIdIndex)
        {
            log.LogInformation("Started SystemStatsJob");
            List<SystemStats> browserInfoFromInteractionArgs = await _ctx.Interactions.Where(
                x => x.WebSiteId == allSiteIds[SiteIdIndex] &&
                x.CreatedAt <= now &&
                x.CreatedAt > ThirtyMinAgo)
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
            log.LogInformation("Finished SystemStatsJob");
        }
    }
}
