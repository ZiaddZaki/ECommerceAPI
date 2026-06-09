using ECommerceAPI.Common;
using ECommerceAPI.DAL;
using System.Threading.Tasks;

namespace ECommerceAPI.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductReadDTo>> GetProductsAsync()
        {

            var products = await _unitOfWork.ProductRepository.GetProductsWithCategoryAsync();
            return products.Select(p => new ProductReadDTo
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
            });


        }
        public async Task<PagedResult<Product>> GetProductsPagenationAsync(PaginationParameters paginationParameters, PrdouctFilterParameters? prdouctFilterParameters)
        {
            var pagedProducts = await _unitOfWork.ProductRepository.GetProductsPagination(paginationParameters, prdouctFilterParameters);
            return pagedProducts;
        }
        public async Task<ProductReadDTo?> GetProductByIdAsync(int id)
        {

            var product = await _unitOfWork.ProductRepository.GetProductByIdWithCategoryAsync(id);

            if (product == null) {
                return null;
            }

            var productDto = new ProductReadDTo { 
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
            };

            return productDto;

        }
        public async Task<ProductReadDTo?> CreateProduct(ProductCreateDTo NewProduct)
        {

            if(NewProduct == null)
            {
                return null;
            }
            var product = new Product
            {
                Name = NewProduct.Name,
                Price = NewProduct.Price,
                Description = NewProduct.Description,
                ImageUrl = NewProduct.ImageUrl,
                Stock = NewProduct.Stock,
                CategoryId = NewProduct.CategoryId,
            };
            _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();
            var productDto = new ProductReadDTo
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
            };
            return productDto;
        }
        public async Task<ProductEditDTo?> EditProduct(int id, ProductEditDTo EditedProduct)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if(EditedProduct == null || product == null)
            {
                return null;
            }

            product.Name = EditedProduct.Name;
            product.Price = EditedProduct.Price;
            product.Description = EditedProduct.Description;
            product.ImageUrl = EditedProduct.ImageUrl;
            product.Stock = EditedProduct.Stock;
            product.CategoryId = EditedProduct.CategoryId;

            await _unitOfWork.SaveChangesAsync();
            return EditedProduct;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var Product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (Product == null) {
                return false;
            }
            _unitOfWork.ProductRepository.Delete(Product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetProductImage(int ProductId, ImageUploadResultDTo imageUploadResultDTo)
        {
            var Product = await _unitOfWork.ProductRepository.GetByIdAsync(ProductId);
            if (Product == null)
            {
                return false;
            }
            Product.ImageUrl  = imageUploadResultDTo.ImageUrl;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
