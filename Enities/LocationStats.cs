using System;

namespace SharpCounter.Enities
{
    public class LocationStats
    {
        public int Id { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Count { get; set; }
        public int CountUnique { get; set; }
    }
}
