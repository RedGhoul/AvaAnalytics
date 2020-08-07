using Application.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Response
{
    public class GetLocationStatsResponse
    {
        public List<LocationStatsDTO> Data { get; set; }
    }
}
