using Application.DTO;
using Domain;
using System.Collections.Generic;

namespace Presentation.Models
{
    public class WebSiteOverviewDTO
    {
        public WebSites Website { get; set; }
        public List<PageViewStatsDTO> PageViewStats { get; set; }
        public List<LocationStatsDTO> LocationStats { get; set; }
    }
}
