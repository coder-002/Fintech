using Fintech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fintech.Data;

public class AppDbContext: IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Portfolio> Portfolios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
        builder.Entity<Portfolio>()
            .HasOne(p => p.AppUser)
            .WithMany(u=> u.Portfolios)
            .HasForeignKey(p=> p.AppUserId);
        
        builder.Entity<Portfolio>()
            .HasOne(p => p.Stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.StockId);
        
        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "1", // ✅ static ID
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2", // ✅ static ID
                Name = "User",
                NormalizedName = "USER"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }

    
    
}