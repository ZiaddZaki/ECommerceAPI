namespace ECommerceAPI.DAL
{
    public class SeedDataProvider
    {
        public static List<Category> GetCategoriesSeed()
        {
            return new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Audio" },
                new Category { Name = "Books" }
            };
        }   
        public static List<Product> GetProductsSeed()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Laptop",
                    Price = 999.99m,
                    Description = "High performance laptop",
                    ImageUrl = "https://example.com/laptop.jpg",
                    Stock = 10,
                    CategoryId = 1
                },
                new Product
                {
                    Name = "Smartphone",
                    Price = 499.99m,
                    Description = "Latest model smartphone",
                    ImageUrl = "https://example.com/smartphone.jpg",
                    Stock = 20,
                    CategoryId = 1
                },
                new Product
                {
                    Name = "Headphones",
                    Price = 199.99m,
                    Description = "Noise-cancelling headphones",
                    ImageUrl = "https://example.com/headphones.jpg",
                    Stock = 15,
                    CategoryId = 2
                }
            };
        }
    }
}
