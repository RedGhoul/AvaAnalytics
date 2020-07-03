using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class BrowserStats
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Browser { get; set; }
        public string Version { get; set; }
        public int Count { get; set; }
        public int CountUnique { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }

    }
}
