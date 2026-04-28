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

        public virtual DbSet<AppUser> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }
        public virtual DbSet<JobListing> JobListings { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<PickupLocation> PickupLocations { get; set; }
        public virtual DbSet<SavedListing> SavedListings { get; set; }
        public virtual DbSet<SearchQuery> SearchQueries { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketAttachment> TicketAttachments { get; set; }
        public virtual DbSet<TicketMessage> TicketMessages { get; set; }
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

        //later

        //public virtual DbSet<Notification> Notifications { get; set; }


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
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => new { s.UserId, s.ListingId }).IsUnique();

            });

            //category

            modelBuilder.Entity<Category>()
               .Property(c => c.ItemsCount)
               .HasDefaultValue(0)
               .ValueGeneratedOnAdd();

            //job Listing

            modelBuilder.Entity<JobListing>(entity =>
            {
                entity.HasIndex(j => j.OwnerId);
                entity.HasIndex(j => j.LocationId);
                entity.HasIndex(j => j.Status);
                entity.HasIndex(j => j.CreatedAt);

                entity.HasOne(j => j.Owner)
                    .WithMany(u => u.JobListings)
                    .HasForeignKey(j => j.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //chat

            modelBuilder.Entity<Chat>(entity =>
            {
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
            });

            //chat messages

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasIndex(m => m.ChatId);
                entity.HasIndex(m => m.SentAt);

                entity.HasOne(m => m.Sender)
                    .WithMany()
                    .HasForeignKey(m => m.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Receiver)
                    .WithMany()
                    .HasForeignKey(m => m.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);
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
            });


            //ticket messages

            modelBuilder.Entity<TicketMessage>(entity =>
            {
                entity.HasOne(tm => tm.Sender)
                    .WithMany(u => u.TicketMessages)
                    .HasForeignKey(tm => tm.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(tm => tm.Receiver)
                    .WithMany()
                    .HasForeignKey(tm => tm.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            //subcategory

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasOne(s => s.Category)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(s => s.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(s => s.ItemsCount)
                .HasDefaultValue(0)
                .ValueGeneratedOnAdd();
            });

            //user logs

            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.HasIndex(l => l.ActorId);
                entity.HasIndex(l => l.Timestamp);
            });
            #endregion


            #region seedata

            foreach (var item in UserSeed.Data) modelBuilder.Entity<AppUser>().HasData(item);
            foreach (var item in CategorySeed.Data) modelBuilder.Entity<Category>().HasData(item);
            foreach (var item in SubCategorySeed.Data) modelBuilder.Entity<SubCategory>().HasData(item);
            foreach (var item in PickupLocationSeed.Data) modelBuilder.Entity<PickupLocation>().HasData(item);
            foreach (var item in ListingSeed.Data) modelBuilder.Entity<Listing>().HasData(item);
            foreach (var item in JobListingSeed.Data) modelBuilder.Entity<JobListing>().HasData(item);
            foreach (var item in ChatSeed.Data) modelBuilder.Entity<Chat>().HasData(item);
            foreach (var item in ChatMessageSeed.Data) modelBuilder.Entity<ChatMessage>().HasData(item);
            foreach (var item in SavedListingSeed.Data) modelBuilder.Entity<SavedListing>().HasData(item);
            foreach (var item in SearchQuerySeed.Data) modelBuilder.Entity<SearchQuery>().HasData(item);
            foreach (var item in TicketSeed.Data) modelBuilder.Entity<Ticket>().HasData(item);
            foreach (var item in TicketAttachmentSeed.Data) modelBuilder.Entity<TicketAttachment>().HasData(item);
            foreach (var item in TicketMessageSeed.Data) modelBuilder.Entity<TicketMessage>().HasData(item);
            foreach (var item in UserActivityLogSeed.Data) modelBuilder.Entity<UserActivityLog>().HasData(item);
            foreach (var item in UserPreferenceSeed.Data) modelBuilder.Entity<UserPreference>().HasData(item);
            foreach (var item in UserReportSeed.Data) modelBuilder.Entity<UserReport>().HasData(item);
            #endregion


            //modelBuilder.Entity<Notification>(entity =>
            //{
            //    entity.HasIndex(n => n.UserId);
            //    entity.HasIndex(n => n.CreatedAt);
            //});
            //foreach (var item in NotificationSeed.Data) modelBuilder.Entity<Notification>().HasData(item);

        }
    }
}
