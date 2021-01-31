using Application.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class WebSiteOverviewDTO
    {
        public WebSites Website { get; set; }
        public List<PageViewStatsDTO> PageViewStats { get; set; }
        public List<LocationStatsDTO> LocationStats { get; set; }
    }
}
