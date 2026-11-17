using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp2.Data;
using tp2.Models;

namespace tp2.Controllers;

public class MovieController : Controller
{
    private readonly ApplicationDbContext _db;
    
    public MovieController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index(string? sortOrder, int page = 1, int pageSize = 5)
    {
    
        ViewBag.CurrentSort = sortOrder;

        ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";

        ViewBag.GenreSortParm = sortOrder == "genre_desc" ? "genre" : "genre_desc";


        IQueryable<Movie> query = _db.Movies
            .AsNoTracking()
            .Include(m => m.Genre);

        query = sortOrder switch
        {
            "name" => query.OrderBy(m => m.Name),
            "name_desc" => query.OrderByDescending(m => m.Name),

            "genre" => query.OrderBy(m => m.Genre!.Name),
            "genre_desc" => query.OrderByDescending(m => m.Genre!.Name),

            _ => query.OrderBy(m => m.Name)
        };


        int totalMovies = await query.CountAsync();

        pageSize = Math.Clamp(pageSize, 1, 50);

        int totalPages = (int)Math.Ceiling((double)totalMovies / pageSize);

        page = Math.Clamp(page, 1, Math.Max(1, totalPages));


        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;


        var movies = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return View(movies);
    }
    
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _db.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        return View(movie);
    }

    public async Task<IActionResult> Create()
    {
        await LoadGenres();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,GenreId")] Movie movie)
    {
        if (!ModelState.IsValid)
        {
            await LoadGenres(movie.GenreId);
            return View(movie);
        }

        _db.Movies.Add(movie);

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _db.Movies.FindAsync(id);

        if (movie == null) return NotFound();

        await LoadGenres(movie.GenreId);

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GenreId")] Movie movie)
    {
        if (id != movie.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await LoadGenres(movie.GenreId);
            return View(movie);
        }

        _db.Movies.Update(movie);

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var movie = await _db.Movies
            .Include(m => m.Genre)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null) return NotFound();

        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _db.Movies.FindAsync(id);

        if (movie == null) return NotFound();

        _db.Movies.Remove(movie);

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadGenres(int? selected = null)
    {
        var genres = await _db.Genres
                                .AsNoTracking()
                                .OrderBy(g => g.Name)
                                .ToListAsync();

        ViewBag.Genres = new SelectList(genres, "Id", "Name", selected);
    }
}
