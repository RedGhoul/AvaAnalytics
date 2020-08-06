using System;

namespace SharpCounter.Enities
{
    public class InteractionCounts
    {
        public int Id { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public int InteractionStatsId { get; set; }
        public InteractionStats InteractionStats { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
    }
}
