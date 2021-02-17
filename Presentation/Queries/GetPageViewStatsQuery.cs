using Application.Response;
using MediatR;
using System;

namespace Application.Queries
{
    public class GetPageViewStatsQuery : IRequest<GetPageViewStatsResponse>
    {
        public GetPageViewStatsQuery(int webSiteId)
        {
            this.WebSiteId = webSiteId;
        }

        public int WebSiteId { get; set; }
        public string TimeZone { get; set; }
        public DateTime CurrentEndDate { get; set; }
        public DateTime CurrentStartDate { get; set; }
    }
}
