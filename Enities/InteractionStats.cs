using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class InteractionStats
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public WebSites Website { get; set; }
        public string Title { get; set; }

    }
}
