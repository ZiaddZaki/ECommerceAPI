namespace ECommerceAPI.BLL
{
    public class ProductReadDTo
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

    }
}
