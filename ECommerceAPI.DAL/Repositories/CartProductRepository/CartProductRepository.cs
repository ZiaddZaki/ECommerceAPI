namespace ECommerceAPI.DAL
{
    public class CartProductRepository : GenericRepository<CartProduct>, ICartProductRepository
    {
        public CartProductRepository(AppDbContext context) : base(context) { }

    }
}
