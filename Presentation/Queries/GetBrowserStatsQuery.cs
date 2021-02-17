using Application.Response;
using MediatR;
using System;

namespace Application.Queries
{
    public class GetBrowserStatsQuery : IRequest<GetBrowserStatsResponse>
    {
        public GetBrowserStatsQuery(int webSiteId)
        {
            this.WebSiteId = webSiteId;
        }

        public int WebSiteId { get; set; }
        public DateTime CurrentEndDate { get; set; }
        public DateTime CurrentStartDate { get; set; }
    }
}
