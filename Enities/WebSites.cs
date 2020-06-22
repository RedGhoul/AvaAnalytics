using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class WebSites
    {
        public WebSites()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string APIKey { get; set; }
        public string LinkDomain { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ApplicationUser Owner { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<InteractionStats> InteractionStats { get; set; }
        public ICollection<InteractionCounts> InteractionCounts { get; set; }
        public ICollection<BrowserStats> BrowserStats { get; set; }
        public ICollection<LocationStats> LocationStats { get; set; }
    }
}
