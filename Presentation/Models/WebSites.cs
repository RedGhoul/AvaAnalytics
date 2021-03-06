﻿using System;
using System.Collections.Generic;

namespace Domain
{
    public class WebSites
    {
        public WebSites()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string APIKey { get; set; }
        public string HomePageLink { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public ICollection<PageViewStats> PageViewStats { get; set; }
        public ICollection<Interaction> Interactions { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<InteractionPathGroupStats> InteractionPathGroupStats { get; set; }
        public ICollection<InteractionByPathCounts> InteractionByPathCounts { get; set; }
        public ICollection<BrowserStats> BrowserStats { get; set; }
        public ICollection<LocationStats> LocationStats { get; set; }
        public ICollection<SystemStats> SystemStats { get; set; }
        public ICollection<ScreenSizeStats> ScreenSizeStats { get; set; }

    }
}
