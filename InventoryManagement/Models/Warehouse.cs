using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InventoryManagement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
