using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductsController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.Include(p => p.Warehouse).ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Products.Include(x => x.Warehouse).FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Warehouses = await _db.Warehouses.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            if (!ModelState.IsValid) { ViewBag.Warehouses = await _db.Warehouses.ToListAsync(); return View(model); }
            _db.Products.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound();
            ViewBag.Warehouses = await _db.Warehouses.ToListAsync();
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid) { ViewBag.Warehouses = await _db.Warehouses.ToListAsync(); return View(model); }
            _db.Products.Update(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p != null) { _db.Products.Remove(p); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}
