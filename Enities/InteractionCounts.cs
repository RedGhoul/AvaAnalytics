using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class InteractionCounts
    {
        public WebSites WebSite { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public DateTime Hour { get; set; }
        public int Total { get; set; }
        public int TotalUnique { get; set; }
    }
}
