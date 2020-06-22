using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class LocationStats
    {
        public WebSites Website { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Count { get; set; }
        public int CountUnique { get; set; }
    }
}
