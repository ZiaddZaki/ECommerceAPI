using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ECommerceAPI.DAL
{
    public static class DALServiceExtention
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EcommerceApi");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                .UseAsyncSeeding(async (context, _, _) =>
                {
                    if (await context.Set<Category>().AnyAsync()) return;
                    if (await context.Set<Product>().AnyAsync()) return;
                    var products = SeedDataProvider.GetProductsSeed();
                    var categories = SeedDataProvider.GetCategoriesSeed();
                    await context.AddRangeAsync(products);
                    await context.AddRangeAsync(categories);
                    await context.SaveChangesAsync();
                })
                .UseSeeding((context, _) =>
                {
                    if (context.Set<Category>().Any()) return;
                    if (context.Set<Product>().Any()) return;
                    var products = SeedDataProvider.GetProductsSeed();
                    var categories = SeedDataProvider.GetCategoriesSeed();
                    context.AddRange(products);
                    context.AddRange(categories);
                    context.SaveChanges();
                });
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartProductRepository, CartProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderProductRepository, OrderProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
