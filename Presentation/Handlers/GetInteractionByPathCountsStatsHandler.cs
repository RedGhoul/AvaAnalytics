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
    public class GetInteractionByPathCountsStatsHandler : IRequestHandler<GetInteractionByPathCountsStatsQuery, GetInteractionByPathCountsStatsResponse>
    {
        private readonly StatsRepo _statsRepo;
        public GetInteractionByPathCountsStatsHandler(StatsRepo statsRepo)
        {
            _statsRepo = statsRepo;
        }
        public async Task<GetInteractionByPathCountsStatsResponse> Handle(GetInteractionByPathCountsStatsQuery request, CancellationToken cancellationToken)
        {
            DateTime curTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentEndDate);
            DateTime oldTime = TimeZoneInfo.ConvertTimeToUtc(request.CurrentStartDate);
            return new GetInteractionByPathCountsStatsResponse() 
            { Data = await _statsRepo.GetInteractionByPathCounts(curTime, oldTime, request.WebSiteId) };
        }
    }
}
