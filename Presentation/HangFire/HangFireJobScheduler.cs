using Hangfire;
using System;

namespace Presentation.HangFire
{
    public class HangFireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {

            RecurringJob.RemoveIfExists(nameof(BrowserStatsJob));
            RecurringJob.AddOrUpdate<BrowserStatsJob>(nameof(BrowserStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/5 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(SystemStatsJob));
            RecurringJob.AddOrUpdate<SystemStatsJob>(nameof(SystemStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/5 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(InteractionByPathStatsJob));
            RecurringJob.AddOrUpdate<InteractionByPathStatsJob>(nameof(InteractionByPathStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/5 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(LocationStatsJob));
            RecurringJob.AddOrUpdate<LocationStatsJob>(nameof(LocationStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/5 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(ScreenSizeStatsJob));
            RecurringJob.AddOrUpdate<ScreenSizeStatsJob>(nameof(ScreenSizeStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/5 * * * *", TimeZoneInfo.Local);

            RecurringJob.RemoveIfExists(nameof(PageViewStatsJob));
            RecurringJob.AddOrUpdate<PageViewStatsJob>(nameof(PageViewStatsJob),
                job => job.Run(JobCancellationToken.Null),
                "*/5 * * * *", TimeZoneInfo.Local);

            // RecurringJob.RemoveIfExists(nameof(KeepAliveJob));
            // RecurringJob.AddOrUpdate<KeepAliveJob>(nameof(KeepAliveJob),
            //     job => job.Run(JobCancellationToken.Null),
            //     Cron.Minutely(), TimeZoneInfo.Local);
        }
    }
}
