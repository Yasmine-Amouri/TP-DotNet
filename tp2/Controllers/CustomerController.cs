using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp2.Data;
using tp2.ViewModels;
using tp2.Models;

namespace tp2.Controllers;

public class CustomerController : Controller
{
    private readonly RentalDbContext _db;
    public CustomerController(RentalDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var query = _db.Customers
            .AsNoTracking()
            .Include(c => c.MembershipType)
            .OrderBy(c => c.Name);

        Console.WriteLine(query.ToQueryString());

        return View(await query.ToListAsync());
    }

    public async Task<IActionResult> IndexVm()
    {
        var query = _db.Customers
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CustomerListItemViewModel
            {
                Name = c.Name,
                Email = c.Email,
                MembershipName = c.MembershipType!.Name,
                DiscountPercent = c.MembershipType.DiscountPercent
            });

        Console.WriteLine(query.ToQueryString());

        return View(await query.ToListAsync());
    }

    public IActionResult Create()
    {
        LoadMemberships();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Name,Email,MembershipTypeId")] Customer c)
    {
        if (!ModelState.IsValid)
        {
            LoadMemberships();
            ViewBag.Errors = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .ToList();

            return View(c);
        }

        _db.Customers.Add(c);

        _db.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    private void LoadMemberships()
    {
        ViewBag.Memberships = _db.MembershipTypes
                                .AsNoTracking()
                                .Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() })
                                .ToList();
    }
}