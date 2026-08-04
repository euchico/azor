using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class AzorDbContext(DbContextOptions<AzorDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>(user =>
        {
            user.ToTable("Users");
            user.HasKey(applicationUser => applicationUser.Id);

            user.Property(applicationUser => applicationUser.Id).HasMaxLength(450);
            user.Property(applicationUser => applicationUser.UserName).HasMaxLength(256);
            user.Property(applicationUser => applicationUser.NormalizedUserName).HasMaxLength(256);
            user.Property(applicationUser => applicationUser.Email).HasMaxLength(256);
            user.Property(applicationUser => applicationUser.NormalizedEmail).HasMaxLength(256);
            user.Property(applicationUser => applicationUser.ConcurrencyStamp).IsConcurrencyToken();

            user.HasIndex(applicationUser => applicationUser.NormalizedEmail)
                .HasDatabaseName("EmailIndex");
            user.HasIndex(applicationUser => applicationUser.NormalizedUserName)
                .IsUnique()
                .HasDatabaseName("UserNameIndex")
                .HasFilter("[NormalizedUserName] IS NOT NULL");
        });
    }
}
