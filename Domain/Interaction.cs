using System;

namespace Domain
{
    public class Interaction
    {
        public Interaction()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Browser { get; set; }
        public string Location { get; set; }
        public string Language { get; set; }
        public bool FirstVisit { get; set; }
        public string Referrer { get; set; }
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public double DevicePixelRatio { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
