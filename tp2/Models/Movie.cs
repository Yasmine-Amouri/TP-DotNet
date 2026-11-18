
using System.ComponentModel.DataAnnotations;

namespace tp2.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom du film est obligatoire.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "Le nom du film doit contenir entre 2 et 100 caractères.")]
    public string Name { get; set; } = string.Empty;

    public Guid GenreId { get; set; }

    public Genre? Genre { get; set; }

    [ConcurrencyCheck]
    public bool IsAvailable { get; set; } = true;
}
