
namespace ECommerceAPI.DAL
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderByUserId(string userId, int orederId);
        Task<IEnumerable<Order?>> GetOrdersByUserId(string userId);
    }
}