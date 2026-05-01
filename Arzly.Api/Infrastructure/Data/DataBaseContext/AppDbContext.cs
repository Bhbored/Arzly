using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Infrastructure.Data.SeedData;
using Arzly.Api.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Arzly.Api.Infrastructure.Data.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        #region phase 1 dbsets
        public virtual DbSet<AppUser> Users { get; set; }
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

            #region Fluent api


            #region phase 1 entity conf


            //listings
            modelBuilder.Entity<Listing>(entity =>
            {
                entity.HasIndex(l => l.CategoryId);
                entity.HasIndex(l => l.SubcategoryId);
                entity.HasIndex(l => l.OwnerId);
                entity.HasIndex(l => l.PickupLocationId);
                entity.HasIndex(l => l.Status);
                entity.HasIndex(l => l.CreatedAt);

                entity.HasOne(l => l.Category)
                    .WithMany(c => c.Listings)
                    .HasForeignKey(l => l.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(l => l.SubCategory)
                    .WithMany(s => s.Listings)
                    .HasForeignKey(l => l.SubcategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Owner)
                    .WithMany(u => u.Listings)
                    .HasForeignKey(l => l.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.PickupLocation)
                        .WithMany(p => p.Listings)
                        .HasForeignKey(l => l.PickupLocationId)
                        .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(l => l.RelatedChats)
                        .WithOne(c => c.Listing)
                        .HasForeignKey(c => c.ListingId)
                        .OnDelete(DeleteBehavior.Restrict);

                entity.HasQueryFilter(l => !l.IsDeleted);


            });

            //pickuplocation
            modelBuilder.Entity<PickupLocation>(entity =>
            {
                entity.HasQueryFilter(p => !p.IsDeleted);

                entity.HasOne(p => p.User)
                    .WithMany(u => u.DeliveryLocations)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Listings)
                    .WithOne(l => l.PickupLocation)
                    .HasForeignKey(l => l.PickupLocationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //job Listing

            modelBuilder.Entity<JobListing>(entity =>
            {
                entity.HasIndex(j => j.OwnerId);
                entity.HasIndex(j => j.Status);
                entity.HasIndex(j => j.CreatedAt);
                entity.HasIndex(j => j.ExpiresAt);
                entity.HasIndex(j => j.BaseLocation);
                entity.HasIndex(j => j.LocationTitle);

                entity.HasOne(j => j.Owner)
                    .WithMany()
                    .HasForeignKey(j => j.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.HasMany(j => j.RelatedChats)
                    .WithOne(c => c.JobListing)
                    .HasForeignKey(c => c.JobListingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasQueryFilter(j => !j.IsDeleted);
            });

            //savedListing (join table)

            modelBuilder.Entity<SavedListing>(entity =>
            {

                entity.HasOne(sl => sl.User)
                      .WithMany(u => u.SavedListings)
                      .HasForeignKey(sl => sl.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sl => sl.Listing)
                      .WithMany(l => l.SavedByUsers)
                      .HasForeignKey(sl => sl.ListingId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(s => new { s.UserId, s.ListingId }).IsUnique();
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);


            });

            //category

            modelBuilder.Entity<Category>(entity =>
            {
                //entity.Property(c => c.ItemsCount)
                //    .HasComputedColumnSql("SELECT COUNT(*) FROM Listings WHERE CategoryId = Id");//do it as stored 

                entity.HasMany(c => c.Listings)
                    .WithOne(c => c.Category)
                    .HasForeignKey(l => l.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //subcategory

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasOne(s => s.Category)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(s => s.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                //entity.Property(s => s.ItemsCount).
                //HasComputedColumnSql("SELECT COUNT(*) FROM Listings WHERE SubcategoryId = Id");
            });



            //User report

            modelBuilder.Entity<UserReport>(entity =>
            {
                entity.HasOne(r => r.Reporter)
                    .WithMany(u => u.ReportsMade)
                    .HasForeignKey(r => r.ReporterId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.ReportedUser)
                    .WithMany(u => u.ReportsReceived)
                    .HasForeignKey(r => r.ReportedUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(r => r.ReporterId);
                entity.HasIndex(r => r.ReportedUserId);
                entity.HasIndex(r => r.ChatId);
                entity.HasIndex(r => r.IsResolved);
                entity.HasIndex(r => r.CreatedAt);

                entity.HasQueryFilter(v => v.Reporter != null && !v.Reporter.IsDeleted);
                entity.HasQueryFilter(v => v.ReportedUser != null && !v.ReportedUser.IsDeleted);


            });



            //user logs

            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.HasIndex(l => l.ActorId);
                entity.HasIndex(l => l.Timestamp);

                entity.HasQueryFilter(v => v.Actor != null && !v.Actor.IsDeleted);

            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasQueryFilter(u => !u.IsDeleted);

                entity.HasIndex(u => u.FirebaseUid).IsUnique();
                entity.HasIndex(u => u.Email);
                entity.HasIndex(u => u.PublicName);
            });

            // SearchQuery
            modelBuilder.Entity<SearchQuery>(entity =>
            {
                entity.HasOne(sq => sq.User)
                    .WithMany(u => u.SearchHistory)
                    .HasForeignKey(sq => sq.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(sq => sq.UserId);
                entity.HasIndex(sq => sq.SearchedAt);
                entity.HasQueryFilter(v => v.User != null && !v.User.IsDeleted);

            });

            // UserPreference
            modelBuilder.Entity<UserPreference>(entity =>
            {
                entity.HasOne(up => up.User)
                    .WithOne(u => u.Preferences)
                    .HasForeignKey<UserPreference>(up => up.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.User != null && !v.User.IsDeleted);

            });

            // ListingOwned entities - one-to-one relationships
            modelBuilder.Entity<BabyChildDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.BabyChildDetails)
                    .HasForeignKey<BabyChildDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<ElectronicsDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.ElectronicsDetails)
                    .HasForeignKey<ElectronicsDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<FashionDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.FashionDetails)
                    .HasForeignKey<FashionDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<FurnitureDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.FurnitureDetails)
                    .HasForeignKey<FurnitureDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<HobbiesDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.HobbiesDetails)
                    .HasForeignKey<HobbiesDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<PetsDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.PetsDetails)
                    .HasForeignKey<PetsDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<PhonesDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.PhonesDetails)
                    .HasForeignKey<PhonesDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<RealEstateDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.RealEstateDetails)
                    .HasForeignKey<RealEstateDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<ServicesDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.ServicesDetails)
                    .HasForeignKey<ServicesDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<SportsDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.SportsDetails)
                    .HasForeignKey<SportsDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });

            modelBuilder.Entity<VehiclesDetails>(entity =>
            {
                entity.HasOne(e => e.Listing)
                    .WithOne(l => l.VehiclesDetails)
                    .HasForeignKey<VehiclesDetails>(e => e.ListingId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
            });
            #endregion

            #region phase 2 entity conf

            //chat

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasQueryFilter(c => !c.IsDeleted);

                entity.HasIndex(c => c.InitiatorId);
                entity.HasIndex(c => c.ReceiverId);

                entity.HasOne(c => c.Initiator)
                    .WithMany(u => u.ChatsInitiated)
                    .HasForeignKey(c => c.InitiatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Receiver)
                    .WithMany(u => u.ChatsReceived)
                    .HasForeignKey(c => c.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Listing)
                       .WithMany(l => l.RelatedChats)
                       .HasForeignKey(c => c.ListingId)
                       .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.JobListing)
                     .WithMany(j => j.RelatedChats)
                    .HasForeignKey(c => c.JobListingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //chat messages

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasQueryFilter(cm => !cm.IsDeleted);

                entity.HasIndex(m => m.ChatId);
                entity.HasIndex(m => m.SentAt);

                entity.HasOne(cm => cm.Chat)
                       .WithMany(c => c.Messages)
                       .HasForeignKey(cm => cm.ChatId)
                       .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(m => m.Sender)
                    .WithMany()
                    .HasForeignKey(m => m.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Receiver)
                    .WithMany()
                    .HasForeignKey(m => m.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            //tickets

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasOne(t => t.User)
                    .WithMany(u => u.CreatedTickets)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.AssignedTo)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedToId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.RelatedListing)
                    .WithMany()
                    .HasForeignKey(t => t.ListingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(t => t.ListingId);

                entity.HasQueryFilter(v => v.User != null && !v.User.IsDeleted);

            });

            //ticket messages

            modelBuilder.Entity<TicketMessage>(entity =>
            {
                entity.HasOne(tm => tm.Ticket)
                    .WithMany(t => t.Messages)
                    .HasForeignKey(tm => tm.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tm => tm.Sender)
                    .WithMany(u => u.TicketMessages)
                    .HasForeignKey(tm => tm.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(tm => tm.Receiver)
                    .WithMany()
                    .HasForeignKey(tm => tm.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(tm => tm.TicketId);
                entity.HasIndex(tm => tm.SentAt);


            });

            // TicketAttachment
            modelBuilder.Entity<TicketAttachment>(entity =>
            {
                entity.HasOne(ta => ta.Ticket)
                    .WithMany(t => t.Attachments)
                    .HasForeignKey(ta => ta.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(ta => ta.TicketId);
                entity.HasIndex(ta => ta.UploadedAt);
            });

            // Chat - additional indexes
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasIndex(c => c.ListingId);
                entity.HasIndex(c => c.JobListingId);
                entity.HasIndex(c => c.LastActivity);
                entity.HasIndex(c => c.IsArchived);
            });

            #endregion


            #endregion


            #region seedata

            foreach (var item in AppUserSeed.Users) modelBuilder.Entity<AppUser>().HasData(item);
            foreach (var item in CategorySeed.Data) modelBuilder.Entity<Category>().HasData(item);
            foreach (var item in SubCategorySeed.Data) modelBuilder.Entity<SubCategory>().HasData(item);
            foreach (var item in PickupLocationSeed.Data) modelBuilder.Entity<PickupLocation>().HasData(item);
            foreach (var item in ListingSeed.Data) modelBuilder.Entity<Listing>().HasData(item);
            foreach (var item in JobListingSeed.Data) modelBuilder.Entity<JobListing>().HasData(item);
            foreach (var item in SavedListingSeed.Data) modelBuilder.Entity<SavedListing>().HasData(item);
            foreach (var item in SearchQuerySeed.Data) modelBuilder.Entity<SearchQuery>().HasData(item);
            foreach (var item in UserActivityLogSeed.Data) modelBuilder.Entity<UserActivityLog>().HasData(item);
            foreach (var item in UserPreferenceSeed.Data) modelBuilder.Entity<UserPreference>().HasData(item);
            foreach (var item in UserReportSeed.Data) modelBuilder.Entity<UserReport>().HasData(item);
            #endregion



        }
    }
}
