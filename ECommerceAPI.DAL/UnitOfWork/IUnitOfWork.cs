
namespace ECommerceAPI.DAL
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartProductRepository CartProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderProductRepository OrderProductRepository { get; }
        Task SaveChangesAsync();
    }
}