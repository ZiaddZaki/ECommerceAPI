using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.BLL
{
    public static class BLLServicesExtention
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddScoped<IErrorMapper, ErrorMapper>();
            services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();


        }
    }
}
