using Application.DTO;
using System.Collections.Generic;

namespace Application.Response
{
    public class GetPageViewStatsResponse
    {
        public List<PageViewStatsDTO> Data { get; set; }
    }
}
