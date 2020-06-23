using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class Session
    {
        public Session()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public WebSites WebSite { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastSeen { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
    }
}
