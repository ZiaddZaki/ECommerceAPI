
using ECommerceAPI.Common;

namespace ECommerceAPI.DAL
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetProductByIdWithCategoryAsync(int id);
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
        Task<PagedResult<Product>> GetProductsPagination(PaginationParameters? paginationParameters, PrdouctFilterParameters? prdouctFilterParameters);

    }
}