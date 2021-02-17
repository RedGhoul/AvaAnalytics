using Application.DTO;
using System.Collections.Generic;

namespace Application.Response
{
    public class GetSystemStatsResponse
    {
        public List<SystemStatsDTO> Data { get; set; }
    }
}
