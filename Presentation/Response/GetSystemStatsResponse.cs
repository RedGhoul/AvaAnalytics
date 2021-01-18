using Application.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Response
{
    public class GetSystemStatsResponse
    {
        public List<SystemStatsDTO> Data { get; set; }
    }
}
