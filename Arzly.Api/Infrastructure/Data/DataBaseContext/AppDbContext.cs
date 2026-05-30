using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Infrastructure.Data.SeedData;
using Arzly.Api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Arzly.Api.Infrastructure.Data.DataBaseContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext()
        {

        }


        #region phase 1 dbsets

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<JobListing> JobListings { get; set; }
        public virtual DbSet<PickupLocation> PickupLocations { get; set; }
        public virtual DbSet<SavedListing> SavedListings { get; set; }
        public virtual DbSet<SearchQuery> SearchQueries { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public virtual DbSet<UserPreference> UserPreferences { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }
        // Listing Owned Details
        public virtual DbSet<BabyChildDetails> BabyChildDetails { get; set; }
        public virtual DbSet<ElectronicsDetails> ElectronicsDetails { get; set; }
        public virtual DbSet<FashionDetails> FashionDetails { get; set; }
        public virtual DbSet<FurnitureDetails> FurnitureDetails { get; set; }
        public virtual DbSet<HobbiesDetails> HobbiesDetails { get; set; }
        public virtual DbSet<PetsDetails> PetsDetails { get; set; }
        public virtual DbSet<PhonesDetails> PhonesDetails { get; set; }
        public virtual DbSet<RealEstateDetails> RealEstateDetails { get; set; }
        public virtual DbSet<ServicesDetails> ServicesDetails { get; set; }
        public virtual DbSet<SportsDetails> SportsDetails { get; set; }
        public virtual DbSet<VehiclesDetails> VehiclesDetails { get; set; }

        #endregion

        #region phase 2 dbsets

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketAttachment> TicketAttachments { get; set; }
        public virtual DbSet<TicketMessage> TicketMessages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning);
                warnings.Ignore(RelationalEventId.OptionalDependentWithAllNullPropertiesWarning);
            });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "support", NormalizedName = "SUPPORT" },
                new ApplicationRole { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "user", NormalizedName = "USER" }
            );

            foreach (var item in ApplicationUserSeed.Users) modelBuilder.Entity<ApplicationUser>().HasData(item);
            foreach (var item in CategorySeed.Data) modelBuilder.Entity<Category>().HasData(item);
            foreach (var item in SubCategorySeed.Data) modelBuilder.Entity<SubCategory>().HasData(item);
        }
    }
}
