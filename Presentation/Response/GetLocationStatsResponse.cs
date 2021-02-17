using Application.DTO;
using System.Collections.Generic;

namespace Application.Response
{
    public class GetLocationStatsResponse
    {
        public List<LocationStatsDTO> Data { get; set; }
    }
}
