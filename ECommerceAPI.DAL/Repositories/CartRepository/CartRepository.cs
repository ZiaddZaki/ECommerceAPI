using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceAPI.DAL
{ 
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            var CartById = await _context.Carts
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (CartById == null) {
                return null;
                
            }
            return CartById;
        }

    }
}
