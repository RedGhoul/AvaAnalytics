using Hangfire;
using Microsoft.Extensions.Logging;
using System;

namespace Presentation.HangFire
{
    public class KeepAliveJob
    {
        private readonly ILogger<KeepAliveJob> _logger;

        public KeepAliveJob(ILogger<KeepAliveJob> logger)
        {
            _logger = logger;
        }
        public void Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            RunAtTimeOf(DateTime.Now);
        }

        public void RunAtTimeOf(DateTime now)
        {
            _logger.LogInformation("KeepAliveJob started");
        }
    }
}
