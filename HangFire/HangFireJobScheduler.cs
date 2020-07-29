using Hangfire;
using SharpCounter.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.HangFire
{
    public class HangFireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {

            RecurringJob.RemoveIfExists(nameof(BrowserStatsJob));
            RecurringJob.AddOrUpdate<BrowserStatsJob>(nameof(BrowserStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/30 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(SystemStatsJob));
            RecurringJob.AddOrUpdate<SystemStatsJob>(nameof(SystemStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/32 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(InteractionStatsJob));
            RecurringJob.AddOrUpdate<InteractionStatsJob>(nameof(InteractionStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/34 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(LocationStatsJob));
            RecurringJob.AddOrUpdate<LocationStatsJob>(nameof(LocationStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/36 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(ScreenSizeStatsJob));
            RecurringJob.AddOrUpdate<ScreenSizeStatsJob>(nameof(ScreenSizeStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/38 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(PageViewStatsJob));
            RecurringJob.AddOrUpdate<PageViewStatsJob>(nameof(PageViewStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/40 * * * *", TimeZoneInfo.Local);
        }
    }
}
