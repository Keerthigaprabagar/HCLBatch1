using System;

namespace InventoryManagement.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Change { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Note { get; set; }
    }
}
