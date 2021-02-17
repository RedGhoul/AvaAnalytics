using Application.Response;
using MediatR;
using System;

namespace Application.Queries
{
    public class GetSystemStatsQuery : IRequest<GetSystemStatsResponse>
    {
        public GetSystemStatsQuery(int webSiteId)
        {
            this.WebSiteId = webSiteId;
        }

        public int WebSiteId { get; set; }
        public DateTime CurrentEndDate { get; set; }
        public DateTime CurrentStartDate { get; set; }
    }
}
