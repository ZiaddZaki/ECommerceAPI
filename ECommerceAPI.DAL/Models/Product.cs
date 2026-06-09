namespace ECommerceAPI.DAL
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<CartProduct>? CartProducts { get; set; } = new HashSet<CartProduct>();
        public ICollection<OrderProduct>? OrderProducts { get; set; } = new HashSet<OrderProduct>();

    }
}
