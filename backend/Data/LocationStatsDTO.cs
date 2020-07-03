using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Data
{
    public class LocationStatsDTO
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Count { get; set; }
    }
}
