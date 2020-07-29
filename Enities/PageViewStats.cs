using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class PageViewStats
    {
        public PageViewStats()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public double Count { get; set; }
        public DateTime CreatedAt { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
    }
}
