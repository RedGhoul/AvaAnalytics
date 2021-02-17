using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Presentation.ViewModels
{
    public class WebSiteDetailsViewModel
    {
        public WebSites WebSite { get; set; }
        public List<SelectListItem> TimeZoneValues { get; set; }

        public string SelectedTimeZone { get; set; }
    }
}
