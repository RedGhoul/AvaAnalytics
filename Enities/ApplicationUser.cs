using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SharpCounter.Enities
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

        public ICollection<WebSites> Websites { get; set; }
    }
}
