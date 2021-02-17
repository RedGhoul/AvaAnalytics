using Application.DTO;
using System.Collections.Generic;

namespace Application.Response
{
    public class GetScreenSizeStatsResponse
    {
        public List<ScreenSizeStatsDTO> Data { get; set; }
    }
}
