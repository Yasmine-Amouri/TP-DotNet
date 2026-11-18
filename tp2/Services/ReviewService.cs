using MongoDB.Driver;
using tp2.Models;

namespace tp2.Services;

public class ReviewService
{
    private readonly IMongoCollection<Review> _reviews;

    public ReviewService(IMongoClient client, IConfiguration config)
    {
        var db = client.GetDatabase(config["MongoDb:Database"]);
        _reviews = db.GetCollection<Review>("reviews");
    }

    public async Task<List<Review>> GetByMovieAsync(int movieId)
        => await _reviews.Find(r => r.MovieId == movieId)
                         .SortByDescending(r => r.CreatedAt)
                         .ToListAsync();

    public Task AddAsync(Review review) => _reviews.InsertOneAsync(review);

    public async Task<double> AverageRatingAsync(int movieId)
    {
        var result = await _reviews.Aggregate()
            .Match(r => r.MovieId == movieId)
            .Group(r => r.MovieId, g => new { Avg = g.Average(x => x.Rating) })
            .FirstOrDefaultAsync();
        return result?.Avg ?? 0;
    }
}