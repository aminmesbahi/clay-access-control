using Microsoft.EntityFrameworkCore;

namespace Clay.AccessControl.Api.Data;
public class AccessControlDbContext : DbContext, IAccessControlDbContext
{
    public AccessControlDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<Lock> Locks { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Audit> Audits { get; set; } = default!;

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

        modelBuilder.Entity<Lock>().Property(e => e.Description).HasMaxLength(256);
        modelBuilder.Entity<User>().Property(e => e.Name).HasMaxLength(35);
        modelBuilder.Entity<Audit>().Property(e => e.LockDescription).HasMaxLength(256);
        modelBuilder.Entity<Audit>().Property(e => e.UserName).HasMaxLength(35);
        modelBuilder.Entity<Audit>().Property(e => e.ClientIp).HasMaxLength(15);

        #region SeedingData
        modelBuilder.Entity<Lock>().HasData(Seed.Locks);
        modelBuilder.Entity<User>().HasData(Seed.Users);
        modelBuilder.Entity<Tag>().HasData(Seed.Tags);
        modelBuilder.Entity<LockTag>().HasData(Seed.LockTags);
        modelBuilder.Entity<Audit>().HasData(Seed.Audits);
        #endregion

        base.OnModelCreating(modelBuilder);
    }
}