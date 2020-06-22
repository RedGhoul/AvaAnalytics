using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Enities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<WebSites> Websites { get; set; }
    }
}
