using ECommerceAPI.Common;
using ECommerceAPI.DAL;

namespace ECommerceAPI.BLL
{
    public interface IProductManager 
    {
        Task<IEnumerable<ProductReadDTo>> GetProductsAsync();
        Task<ProductReadDTo?> GetProductByIdAsync(int id);
        Task<ProductReadDTo?> CreateProduct(ProductCreateDTo NewProduct);
        Task<ProductEditDTo?> EditProduct(int id, ProductEditDTo EditedProduct);
        Task<bool> DeleteProduct(int id);
        Task<PagedResult<Product>> GetProductsPagenationAsync(PaginationParameters paginationParameters, PrdouctFilterParameters? prdouctFilterParameters);
        Task<bool> SetProductImage(int ProductId, ImageUploadResultDTo imageUploadResultDTo);

    }
}