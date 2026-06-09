using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceAPI.DAL
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order?>> GetOrdersByUserId(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product)
                .Where(o => o.UserId == userId).ToListAsync();

            return orders;
        }
        public async Task<Order?> GetOrderByUserId(string userId, int orederId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orederId);

            return order;
        }
    }
}
