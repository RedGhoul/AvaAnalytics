using Hangfire;
using System;

namespace Presentation.HangFire
{
    public class HangFireJobScheduler
    {
        public static void ScheduleRecurringJobs()
        {

            //RecurringJob.RemoveIfExists(nameof(StatsCollectorJob));
            //RecurringJob.AddOrUpdate<StatsCollectorJob>(nameof(StatsCollectorJob),
            //    job => job.Run(JobCancellationToken.Null),
            //    "*/5 * * * *", TimeZoneInfo.Local);

        }
    }
}
