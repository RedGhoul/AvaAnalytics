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
    public class GetLocationStatsHandler : IRequestHandler<GetLocationStatsQuery, GetLocationStatsResponse>
    {
        private readonly StatsRepo _statsRepo;
        public GetLocationStatsHandler(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }

        public async Task<GetLocationStatsResponse> Handle(GetLocationStatsQuery request, CancellationToken cancellationToken)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentStartDate);
            return new GetLocationStatsResponse()
            { Data = await _statsRepo.GetLocationStats(curTime, oldTime, request.WebSiteId) };
        }
    }
}
