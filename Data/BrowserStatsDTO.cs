using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Data
{
    public class BrowserStatsDTO
    {
        public string Browser { get; set; }
        public string Version { get; set; }
        public int Count { get; set; }
    }
}
