using Application.DTO;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Application.Queries
{
    public class GetInteractionByPathCountsStatsQuery : IRequest<GetInteractionByPathCountsStatsResponse>
    {
        public GetInteractionByPathCountsStatsQuery(int webSiteId)
        {
            this.WebSiteId = webSiteId;
        }

        public int WebSiteId { get; set; }
        public DateTime CurrentEndDate { get; set; }
        public DateTime CurrentStartDate { get; set; }
    }
}
