using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Models;

namespace NetKubernetes.Data;

public class AppDbContext: IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().Property(u => u.Id).HasMaxLength(36);
        builder.Entity<User>().Property(u => u.NormalizedUserName).HasMaxLength(90);
        builder.Entity<IdentityRole>().Property(u => u.Id).HasMaxLength(36);
        builder.Entity<IdentityRole>().Property(u => u.NormalizedName).HasMaxLength(36);
    }
    
    public DbSet<Estate>? Estates { get; set; } 
}