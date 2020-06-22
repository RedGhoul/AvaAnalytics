using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharpCounter.Enities;

namespace SharpCounter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BrowserStats> BrowserStats { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionCounts> InteractionCounts { get; set; }
        public DbSet<InteractionStats> InteractionStats { get; set; }
        public DbSet<LocationStats> LocationStats { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<WebSites> WebSites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Websites)
                .WithOne(e => e.Owner);

            modelBuilder.Entity<WebSites>()
                .Property(c => c.)
                .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
