using Microsoft.EntityFrameworkCore;
using tp2.Data;
using tp2.Models;

namespace tp2.Services;

public class RentalResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

public class RentalService
{
    private readonly ApplicationDbContext _db;
    private readonly RentalDbContext _rentalDb;

    public RentalService(ApplicationDbContext db, RentalDbContext rentalDb)
    {
        _db = db;
        _rentalDb = rentalDb;
    }

    public async Task<RentalResult> RentAsync(int movieId, int customerId)
    {
    
        var movie = await _db.Movies.FindAsync(movieId);

        if (movie == null)
        {
            return new RentalResult
            {
                Success = false,
                ErrorMessage = "Film introuvable."
            };
        }

        if (!movie.IsAvailable)
        {
            return new RentalResult
            {
                Success = false,
                ErrorMessage = "Ce film n'est pas disponible."
            };
        }

        var customer = await _rentalDb.Customers.FindAsync(customerId);

        if (customer == null)
        {
            return new RentalResult
            {
                Success = false,
                ErrorMessage = "Client introuvable."
            };
        }

    
        movie.IsAvailable = false;

        await _db.SaveChangesAsync();

        try
        {
            var rental = new Rental
            {
                MovieId = movieId,
                CustomerId = customerId,
                RentedAt = DateTime.UtcNow
            };

            _rentalDb.Rentals.Add(rental);

            await _rentalDb.SaveChangesAsync();

            return new RentalResult
            {
                Success = true
            };
        }
        catch
        {
    
            movie.IsAvailable = true;
            
            await _db.SaveChangesAsync();

            return new RentalResult
            {
                Success = false,
                ErrorMessage = "Une erreur est survenue lors de la location."
            };
        }
    }
}
