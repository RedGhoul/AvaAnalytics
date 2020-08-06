using System;

namespace SharpCounter.Enities
{
    public class SystemStats
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public int Count { get; set; }
        public int CountUnique { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
    }
}
