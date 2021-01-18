using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Response
{
    public class GetBrowserStatsResponse
    {
        public List<BrowserStatsDTO> Data { get; set; }
    }
}
