using Application.Queries;
using Application.Repository;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetPageViewCountStatsHandler : IRequestHandler<GetPageViewStatsQuery, GetPageViewStatsResponse>
    {
        private readonly StatsRepo _statsRepo;
        public GetPageViewCountStatsHandler(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }

        public async Task<GetPageViewStatsResponse> Handle(GetPageViewStatsQuery request, CancellationToken cancellationToken)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentStartDate);
            return new GetPageViewStatsResponse()
            { Data = await _statsRepo.GetPageViewCountStats(curTime, oldTime, request.WebSiteId) };
        }
    }
}
