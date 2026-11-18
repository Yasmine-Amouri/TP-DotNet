using tp2.Models;

namespace tp2.ViewModels;

public class MovieDetailsViewModel
{
    public Movie Movie { get; set; } = null!;
    public List<Review> Reviews { get; set; } = new();
    public double AverageRating { get; set; }
}