namespace tp2.Models;

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public bool IsAvailable { get; set; } = true;
}