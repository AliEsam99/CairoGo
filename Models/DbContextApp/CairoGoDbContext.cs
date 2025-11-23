using CairoGo.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CairoGo.Models.DbContextApp
{
    public class CairoGoDbContext : IdentityDbContext<UserApplication, IdentityRole<Guid>, Guid>
    {

        public CairoGoDbContext(DbContextOptions<CairoGoDbContext> options) : base(options)
        {
            
        }

        public DbSet<PreferenceProfile> PreferenceProfiles { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceVibeTag> PlaceVibeTags { get; set; }
        public DbSet<PlaceOperatingHours> PlaceOperatingHours { get; set; }
        public DbSet<TrendingTag> TrendingTags { get; set; }
        public DbSet<TripPlan> TripPlans { get; set; }
        public DbSet<TripDay> TripDays { get; set; }
        public DbSet<TripSlot> TripSlots { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RecommendationLog> RecommendationLogs { get; set; }
        public DbSet<SearchSession> SearchSessions { get; set; }
        public DbSet<ModelVersion> ModelVersions { get; set; }
        public DbSet<ExperimentAssignment> ExperimentAssignments { get; set; }
        public DbSet<UserPreferenceSignal> UserPreferenceSignals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================
            // UNIQUE CONSTRAINTS
            // ====================
            modelBuilder.Entity<UserApplication>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ====================
            // MANY-TO-MANY RELATIONSHIPS
            // ====================
            modelBuilder.Entity<ActivityType>()
                .HasMany(a => a.Places)
                .WithMany(p => p.ActivityTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaceActivityTypes",
                    j => j.HasOne<Place>().WithMany().OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<ActivityType>().WithMany().OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<ActivityType>()
                .HasMany(a => a.PreferenceProfiles)
                .WithMany(p => p.ActivityTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "PreferenceActivityTypes",
                    j => j.HasOne<PreferenceProfile>().WithMany().OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<ActivityType>().WithMany().OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<TrendingTag>()
                .HasMany(t => t.Places)
                .WithMany(p => p.TrendingTags)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaceTrendingTags",
                    j => j.HasOne<Place>().WithMany().OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<TrendingTag>().WithMany().OnDelete(DeleteBehavior.Cascade)
                );

            // ====================
            // CHECK CONSTRAINTS
            // ====================
            modelBuilder.Entity<PlaceOperatingHours>()
                .HasCheckConstraint("CK_OperatingHours_ValidTime",
                    "[IsClosed] = 1 OR ([OpenTime] IS NOT NULL AND [CloseTime] IS NOT NULL AND [OpenTime] < [CloseTime])");

            modelBuilder.Entity<TrendingTag>()
                .HasCheckConstraint("CK_TrendingTag_ValidPeriod",
                    "[PeriodStart] < [PeriodEnd]");

            modelBuilder.Entity<TripDay>()
                .HasCheckConstraint("CK_TripDay_PositiveDayNumber",
                    "[DayNumber] > 0");

            // ====================
            // SELF-REFERENCING (TripPlan Alternatives)
            // ====================
            modelBuilder.Entity<TripPlan>()
                .HasOne(t => t.ParentPlan)
                .WithMany(t => t.AlternativePlans)
                .HasForeignKey(t => t.ParentPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // ====================
            // TRIPSLOT MULTIPLE FOREIGN KEYS TO PLACE
            // ====================
            modelBuilder.Entity<TripSlot>()
                .HasOne(s => s.Place)
                .WithMany(p => p.TripSlots)
                .HasForeignKey(s => s.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TripSlot>()
                .HasOne(s => s.AlternatePlace)
                .WithMany(p => p.AlternateTripSlots)
                .HasForeignKey(s => s.AlternatePlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            // ====================
            // USER RELATIONSHIPS (CASCADE)
            // ====================
            modelBuilder.Entity<PreferenceProfile>()
                .HasOne(p => p.User)
                .WithOne(u => u.PreferenceProfile)
                .HasForeignKey<PreferenceProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TripPlan>()
                .HasOne(t => t.User)
                .WithMany(u => u.TripPlans)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); 


            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Interaction>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interactions)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecommendationLog>()
                .HasOne(r => r.User)
                .WithMany(u => u.RecommendationLogs)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExperimentAssignment>()
                .HasOne(e => e.User)
                .WithMany(u => u.ExperimentAssignments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserPreferenceSignal>()
                .HasOne(u => u.User)
                .WithMany(user => user.PreferenceSignals)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ====================
            // PLACE RELATIONSHIPS (RESTRICT - PROTECTED)
            // ====================
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Place)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Interaction>()
                .HasOne(i => i.Place)
                .WithMany(p => p.Interactions)
                .HasForeignKey(i => i.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Place)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecommendationLog>()
                .HasOne(r => r.Place)
                .WithMany(p => p.RecommendationLogs)
                .HasForeignKey(r => r.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            // ====================
            // PLACE METADATA (CASCADE)
            // ====================
            modelBuilder.Entity<PlaceVibeTag>()
                .HasOne(v => v.Place)
                .WithMany(p => p.VibeTags)
                .HasForeignKey(v => v.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlaceOperatingHours>()
                .HasOne(h => h.Place)
                .WithMany(p => p.OperatingHours)
                .HasForeignKey(h => h.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            // ====================
            // TRIP HIERARCHY (CASCADE)
            // ====================
            modelBuilder.Entity<TripDay>()
                .HasOne(d => d.TripPlan)
                .WithMany(t => t.TripDaysCollection)
                .HasForeignKey(d => d.TripPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TripSlot>()
                .HasOne(s => s.TripDay)
                .WithMany(d => d.TripSlots)
                .HasForeignKey(s => s.TripDayId)
                .OnDelete(DeleteBehavior.Cascade);

            // ====================
            // SET NULL RELATIONSHIPS
            // ====================
            modelBuilder.Entity<TripPlan>()
                .HasOne(t => t.PreferenceProfile)
                .WithMany()
                .HasForeignKey(t => t.PreferenceProfileId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RecommendationLog>()
                .HasOne(r => r.TripPlan)
                .WithMany(t => t.RecommendationLogs)
                .HasForeignKey(r => r.TripPlanId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SearchSession>()
                .HasOne(s => s.User)
                .WithMany(u => u.SearchSessions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ====================
            // DEFAULT VALUES
            // ====================
            modelBuilder.Entity<Place>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<TripPlan>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<UserApplication>()
                .Property(u => u.JoinDate)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        // ====================
        // AUTO-UPDATE UpdatedAt
        // ====================
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is TripPlan tripPlan)
                    tripPlan.UpdatedAt = DateTime.UtcNow;

                if (entry.Entity is Place place)
                    place.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
