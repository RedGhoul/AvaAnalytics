using Application.Queries;
using Application.Repository;
using Application.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetSystemStatsHandler : IRequestHandler<GetSystemStatsQuery, GetSystemStatsResponse>
    {
        private readonly StatsRepo _statsRepo;
        public GetSystemStatsHandler(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }

        public async Task<GetSystemStatsResponse> Handle(GetSystemStatsQuery request, CancellationToken cancellationToken)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentStartDate);
            return new GetSystemStatsResponse()
            { Data = await _statsRepo.GetSystemStats(curTime, oldTime, request.WebSiteId) };
        }
    }
}
