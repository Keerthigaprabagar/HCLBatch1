using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public WarehousesController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index() => View(await _db.Warehouses.Include(w => w.Products).ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Warehouse w)
        {
            if (!ModelState.IsValid) return View(w);
            _db.Warehouses.Add(w);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
