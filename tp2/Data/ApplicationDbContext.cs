using Microsoft.EntityFrameworkCore;
using tp2.Models;

namespace tp2.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
        new Genre { Id = 1, Name = "Action" },
        new Genre { Id = 2, Name = "Drame" },
        new Genre { Id = 3, Name = "Comédie" });
    }
}