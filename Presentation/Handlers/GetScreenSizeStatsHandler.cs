using Application.Queries;
using Application.Repository;
using Application.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetScreenSizeStatsHandler : IRequestHandler<GetScreenSizeStatsQuery, GetScreenSizeStatsResponse>
    {
        private readonly StatsRepo _statsRepo;
        public GetScreenSizeStatsHandler(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }

        public Task<GetScreenSizeStatsResponse> Handle(GetScreenSizeStatsQuery request, CancellationToken cancellationToken)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentStartDate);
            return Task.FromResult(new GetScreenSizeStatsResponse()
            { Data = _statsRepo.GetScreenSizeStats(curTime, oldTime, request.WebSiteId) });
        }

    }
}
