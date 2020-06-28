using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class InteractionCounts
    {
        public int Id { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public string Path { get; set; }
        public DateTime Hour { get; set; }
        public int Total { get; set; }
    }
}
