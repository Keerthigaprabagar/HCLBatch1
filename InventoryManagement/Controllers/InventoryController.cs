using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InventoryController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var items = await _db.InventoryItems.Include(i => i.Product).OrderByDescending(i => i.Timestamp).ToListAsync();
            return View(items);
        }

        public async Task<IActionResult> Adjust(int productId)
        {
            var p = await _db.Products.FindAsync(productId);
            if (p == null) return NotFound();
            ViewBag.Product = p;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adjust(int productId, int change, string? note)
        {
            var p = await _db.Products.FindAsync(productId);
            if (p == null) return NotFound();
            p.Quantity += change;
            _db.InventoryItems.Add(new InventoryItem { ProductId = productId, Change = change, Note = note });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
