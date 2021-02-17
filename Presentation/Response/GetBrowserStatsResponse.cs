using Application.DTO;
using System.Collections.Generic;

namespace Application.Response
{
    public class GetBrowserStatsResponse
    {
        public List<BrowserStatsDTO> Data { get; set; }
    }
}
