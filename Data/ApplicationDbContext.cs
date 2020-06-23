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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Websites)
                .WithOne(e => e.Owner);

            modelBuilder.Entity<WebSites>()
                .HasMany(w => w.BrowserStats)
                .WithOne(b =>b.WebSite);

            modelBuilder.Entity<WebSites>()
                .HasMany(w => w.Interactions)
                .WithOne(b => b.WebSite);

            modelBuilder.Entity<WebSites>()
                .HasMany(w => w.InteractionCounts)
                .WithOne(b => b.WebSite);

            modelBuilder.Entity<WebSites>()
                .HasMany(w => w.InteractionStats)
                .WithOne(b => b.WebSite);

            modelBuilder.Entity<WebSites>()
                .HasMany(w => w.LocationStats)
                .WithOne(b => b.WebSite);

            modelBuilder.Entity<WebSites>()
                .HasMany(w => w.Sessions)
                .WithOne(b => b.WebSite);

            modelBuilder.Entity<WebSites>()
                .HasMany(w => w.SystemStats)
                .WithOne(b => b.WebSite);

            modelBuilder.Entity<Session>()
                .HasMany(w => w.Interactions)
                .WithOne(b => b.Session);

        }

    }
}
