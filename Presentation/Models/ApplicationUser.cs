using Microsoft.AspNetCore.Identity;
using Presentation.Models;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserSetting UserSettings { get; set; }
        public ICollection<WebSites> Websites { get; set; }
    }
}
