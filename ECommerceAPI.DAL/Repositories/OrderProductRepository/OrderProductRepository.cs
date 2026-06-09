namespace ECommerceAPI.DAL
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
