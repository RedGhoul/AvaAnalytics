using Application.DTO;
using System.Collections.Generic;

namespace Application.Response
{
    public class GetInteractionByPathCountsStatsResponse
    {
        public List<InteractionByPathCountsDTO> Data { get; set; }
    }
}
