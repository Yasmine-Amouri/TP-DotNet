using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2.Data;
using tp2.Models;

namespace tp2.Controllers;

public class MovieController : Controller
{
    private readonly ApplicationDbContext _db;
    
    public MovieController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
        => View(await _db.Movies.AsNoTracking().ToListAsync());
    
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _db.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        return View(movie);
    }
}
