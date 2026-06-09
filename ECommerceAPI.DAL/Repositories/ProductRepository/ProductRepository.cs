using ECommerceAPI.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<Product?> GetProductByIdWithCategoryAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PagedResult<Product>> GetProductsPagination
            (PaginationParameters? paginationParameters, PrdouctFilterParameters? prdouctFilterParameters)
        {
            var query = _context.Set<Product>().AsQueryable();
            query = query.Include(p => p.Category);

            if(prdouctFilterParameters != null)
            {
               query = ApplayFilter(query,prdouctFilterParameters);
            }

            var totalCount = await query.CountAsync();
            var pageNumber = paginationParameters?.PageNumber?? 1;   //1  //2
            var pageSize = paginationParameters?.PageSize ?? totalCount;     //10 //10
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return new PagedResult<Product>
            {
                Items = items,
                Metadata = new PaginationMetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    HasNext = pageNumber < totalPages,
                    HasPrevious = pageNumber > 1 

                }

            };
        }
        public IQueryable<Product> ApplayFilter(IQueryable<Product> query ,PrdouctFilterParameters prdouctFilterParameters)
        {
            if(prdouctFilterParameters.ProductName != null)
            {
                query = query.Where(p => p.Name.Contains(prdouctFilterParameters.ProductName));
            }
            if(prdouctFilterParameters.CategoryId > 0)
            {
                query = query.Where(p => p.CategoryId == prdouctFilterParameters.CategoryId);
            }
            return query;
        }
    }
}
