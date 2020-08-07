using System;
using System.Collections.Generic;

namespace Domain
{
    public class Session
    {
        public Session()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string SessionUId { get; set; }
        public int WebSiteId { get; set; }
        public WebSites WebSite { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastSeen { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
    }
}
