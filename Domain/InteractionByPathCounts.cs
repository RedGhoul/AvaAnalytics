using System;

namespace Domain
{
    public class InteractionByPathCounts
    {
        public int Id { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public int InteractionPathGroupStatsId { get; set; }
        public InteractionPathGroupStats InteractionPathGroupStats { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
    }
}
