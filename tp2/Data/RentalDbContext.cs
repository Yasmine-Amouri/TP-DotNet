using Microsoft.EntityFrameworkCore;
using tp2.Models;

namespace tp2.Data;

public class RentalDbContext : DbContext
{
    public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<MembershipType> MembershipTypes => Set<MembershipType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("rental");
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.MembershipType)
            .WithMany()
            .HasForeignKey(c => c.MembershipTypeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MembershipType>().HasData(
                new MembershipType { Id = 1, Name = "Standard", DiscountPercent = 0 },
                new MembershipType { Id = 2, Name = "Premium", DiscountPercent = 10 },
                new MembershipType { Id = 3, Name = "Gold", DiscountPercent = 20 });

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Amal Ben Salah", Email = "amal@example.com", MembershipTypeId = 2 },
                new Customer { Id = 2, Name = "Youssef Trabelsi", Email = "youssef@example.com", MembershipTypeId = 1 });
    }
}