using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.ViewModels
{
    public class WebSiteDetailsViewModel
    {
        public WebSites WebSite { get; set; }
        public List<SelectListItem> TimeZoneValues { get; set; }

        public string  SelectedTimeZone { get; set; }
    }
}
