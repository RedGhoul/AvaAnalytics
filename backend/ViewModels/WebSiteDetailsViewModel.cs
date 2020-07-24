using SharpCounter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.ViewModels
{
    public class WebSiteDetailsViewModel
    {
        public IEnumerable<BrowserStatsDTO> BrowserStats { get; set; }
    }
}
