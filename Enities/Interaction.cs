using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class Interaction
    {
        public Interaction()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public WebSites WebSite { get; set; }
        public Session Session { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Browser { get; set; }
        public string  Location { get; set; }
        public bool FirstVisit { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
