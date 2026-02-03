using EpsilonWebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EpsilonWebApp.SQLServer;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.ContactName).IsRequired()
                   .HasMaxLength(100);
            
            entity.Property(e => e.Address).IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasColumnType("varchar(10)");
            entity.Property(e => e.Phone).HasColumnType("varchar(20)");
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(512);

            entity.HasIndex(e => e.Email).IsUnique()
                .IncludeProperties(e => e.PasswordHash);
        });
    }
}