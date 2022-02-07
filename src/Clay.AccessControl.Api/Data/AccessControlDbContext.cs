using Clay.AccessControl.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Clay.AccessControl.Api.Data {
    public class AccessControlDbContext : DbContext, IAccessControlDbContext {
        public AccessControlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Lock> Locks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Tag>()
            .HasOne<User>(t => t.Owner)
            .WithMany(u => u.OwnedTags)
            .HasForeignKey(t => t.OwnerId);

            modelBuilder.Entity<Lock>()
            .HasMany(l => l.AuthorizedTags)
            .WithMany(t => t.OpeningLocks)
            .UsingEntity<LockTag>(
                j => j
                    .HasOne(lt => lt.Tag)
                    .WithMany(t => t.LockTags)
                    .HasForeignKey(pt => pt.TagId),
                j => j
                    .HasOne(lt => lt.Lock)
                    .WithMany(l => l.LockTags)
                    .HasForeignKey(lt => lt.LockId),
                j =>
                {
                    j.Property(lt => lt.CreateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.LockId, t.TagId });
                });

            modelBuilder.Entity<Lock>().Property(l => l.Description).HasMaxLength(256);
            
            #region SeedingData
            modelBuilder.Entity<Lock>().HasData(Seed.Locks);
            modelBuilder.Entity<User>().HasData(Seed.Users);
            modelBuilder.Entity<Tag>().HasData(Seed.Tags);
            modelBuilder.Entity<LockTag>().HasData(Seed.LockTags);
            #endregion
            
            base.OnModelCreating(modelBuilder);
        }
    }
}