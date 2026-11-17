using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2.Data;
using tp2.Models;

namespace tp2.Controllers;

public class GenreController : Controller
{
    private readonly ApplicationDbContext _db;

    public GenreController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
        => View(await _db.Genres.AsNoTracking().ToListAsync());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] Genre genre)
    {
        if (!ModelState.IsValid) return View(genre);

        _db.Genres.Add(genre);

        await _db.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
    }
}