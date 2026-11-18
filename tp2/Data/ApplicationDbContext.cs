using Microsoft.EntityFrameworkCore;
using tp2.Models;
using System.Text.Json;

namespace tp2.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "Seed", "genres.json");
        var genres = JsonSerializer.Deserialize<List<Genre>>(
            File.ReadAllText(path),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new List<Genre>();
        modelBuilder.Entity<Genre>().HasData(genres);

        var action = Guid.Parse("84ca0bcd-082c-49cb-aa77-ea2f1f5f8285");
        var scienceFiction = Guid.Parse("3f2b8c4e-5a1d-4e7b-9c60-1d2e3f4a5b6c");

        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Name = "Inception", GenreId = scienceFiction, IsAvailable = true },
            new Movie { Id = 2, Name = "Gladiator", GenreId = action, IsAvailable = true },
            new Movie { Id = 3, Name = "Interstellar", GenreId = scienceFiction, IsAvailable = true });
    }
}