using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Presentation.Models;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BrowserStats> BrowserStats { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionByPathCounts> InteractionByPathCounts { get; set; }
        public DbSet<InteractionPathGroupStats> InteractionPathGroupStats { get; set; }
        public DbSet<LocationStats> LocationStats { get; set; }
        public DbSet<ScreenSizeStats> ScreenSizeStats { get; set; }
        public DbSet<PageViewStats> PageViewStats { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<WebSites> WebSites { get; set; }
        public DbSet<SiteContent> SiteContents { get; set; }
        public DbSet<Cache> Caches { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<TimeZoneValue> TimeZoneValues { get; set; }
        public DbSet<SystemStats> SystemStats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WebSites>()
                .HasIndex(b => b.APIKey)
                .IsUnique();

            modelBuilder.Entity<WebSites>()
                .HasOne(w => w.Owner)
                .WithMany(w => w.Websites)
                .HasForeignKey(w => w.OwnerId)
                .IsRequired();

            modelBuilder.Entity<BrowserStats>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.BrowserStats)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<BrowserStats>()
                .HasIndex(x => x.Browser);

            modelBuilder.Entity<BrowserStats>()
                .HasIndex(x => x.Date);

            modelBuilder.Entity<Interaction>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.Interactions)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<Interaction>()
                .HasIndex(b => b.CreatedAt);

            modelBuilder.Entity<Interaction>()
                .HasIndex(b => b.FirstVisit);

            modelBuilder.Entity<Interaction>()
                .HasIndex(b => b.Browser);

            modelBuilder.Entity<Interaction>()
                .HasIndex(b => b.Path);

            modelBuilder.Entity<Interaction>()
                .HasIndex(b => b.ScreenHeight);

            modelBuilder.Entity<Interaction>()
                .HasIndex(b => b.ScreenWidth);

            modelBuilder.Entity<Interaction>()
                .HasIndex(b => b.DevicePixelRatio);

            modelBuilder.Entity<InteractionByPathCounts>()
                .HasOne(w => w.InteractionPathGroupStats)
                .WithMany(w => w.InteractionByPathCounts)
                .HasForeignKey(w => w.InteractionPathGroupStatsId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            modelBuilder.Entity<InteractionByPathCounts>()
                .HasIndex(x => x.Path);

            modelBuilder.Entity<InteractionByPathCounts>()
                .HasIndex(x => x.Date);


            modelBuilder.Entity<InteractionPathGroupStats>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.InteractionPathGroupStats)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<InteractionPathGroupStats>()
                .HasIndex(x => x.Date);

            modelBuilder.Entity<LocationStats>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.LocationStats)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<Session>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.Sessions)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<Session>()
                .HasIndex(x => x.CreatedAt);

            modelBuilder.Entity<Session>()
                .HasIndex(x => x.LastSeen);

            modelBuilder.Entity<Session>()
                .HasIndex(x => x.SessionUId);

            modelBuilder.Entity<SystemStats>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.SystemStats)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<SystemStats>()
                .HasIndex(x => x.Platform);

            modelBuilder.Entity<SystemStats>()
                .HasIndex(x => x.Day);

            modelBuilder.Entity<ScreenSizeStats>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.ScreenSizeStats)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<ScreenSizeStats>()
                .HasIndex(x => x.Date);

            modelBuilder.Entity<PageViewStats>()
                .HasOne(w => w.WebSite)
                .WithMany(w => w.PageViewStats)
                .HasForeignKey(w => w.WebSiteId).OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();

            modelBuilder.Entity<PageViewStats>()
                .HasIndex(x => x.CreatedAt);

            modelBuilder.Entity<Cache>()
               .HasIndex(u => u.Key)
               .IsUnique();


            modelBuilder.Entity<ApplicationUser>()
                .HasOne(s => s.UserSettings)
                .WithOne(s => s.ApplicationUser)
                .HasForeignKey<UserSetting>(s => s.ApplicationUserId);

        }

    }
}
