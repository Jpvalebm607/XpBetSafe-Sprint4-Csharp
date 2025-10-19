using Microsoft.EntityFrameworkCore;
using XpBetSafe.Api.Models;

namespace XpBetSafe.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Alert> Alerts => Set<Alert>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        b.Entity<User>()
            .Property(x => x.FullName)
            .HasMaxLength(120)
            .IsRequired();

        b.Entity<Alert>()
            .Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        b.Entity<Alert>()
            .HasOne(a => a.User)
            .WithMany(u => u.Alerts)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
