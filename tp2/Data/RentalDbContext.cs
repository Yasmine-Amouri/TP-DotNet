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
    }
}