using Hangfire;
using System;
using System.Threading.Tasks;

namespace Presentation.HangFire
{
    public interface IMyJob
    {
        public Task RunAtTimeOf(DateTime now);
        public Task Run(IJobCancellationToken token);
    }
}
