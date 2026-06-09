using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceAPI.BLL
{ 
    public class ProductEditDTo
{
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public required int CategoryId { get; set; }
    }
}
