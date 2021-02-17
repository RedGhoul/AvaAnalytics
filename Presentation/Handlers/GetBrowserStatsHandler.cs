using Application.Queries;
using Application.Repository;
using Application.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetBrowserStatsHandler : IRequestHandler<GetBrowserStatsQuery, GetBrowserStatsResponse>
    {
        private readonly StatsRepo _statsRepo;
        public GetBrowserStatsHandler(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }

        public async Task<GetBrowserStatsResponse> Handle(GetBrowserStatsQuery request, CancellationToken cancellationToken)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentStartDate);
            return new GetBrowserStatsResponse() { Data = await _statsRepo.GetBrowserStats(curTime, oldTime, request.WebSiteId) };
        }
    }
}
