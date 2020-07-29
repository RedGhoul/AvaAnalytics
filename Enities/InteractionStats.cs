using System;
using System.Collections.Generic;

namespace SharpCounter.Enities
{
    public class InteractionStats
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public ICollection<InteractionCounts> InteractionCounts { get; set; }
        public int TotalRoutes { get; set; }

    }
}
