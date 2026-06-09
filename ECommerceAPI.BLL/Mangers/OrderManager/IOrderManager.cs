
namespace ECommerceAPI.BLL
{
    public interface IOrderManager
    {
        Task<OrderReadDTo?> GenerateOrder(string userId);
        Task<OrderReadDTo?> GetOrderByUserId(string userId, int orderId);
        Task<IEnumerable<OrderReadDTo>> GetOrdersByUserId(string userId);
    }
}