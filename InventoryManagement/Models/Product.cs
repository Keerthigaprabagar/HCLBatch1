using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public int Quantity { get; set; }
        public int? WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
