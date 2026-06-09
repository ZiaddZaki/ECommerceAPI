namespace ECommerceAPI.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICartProductRepository CartProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderProductRepository OrderProductRepository { get; }

        public UnitOfWork
            (AppDbContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ICartRepository cartRepository,
            ICartProductRepository cartProductRepository,
            IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository
            )
        {
            _context = context;
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            CartRepository = cartRepository;
            CartProductRepository = cartProductRepository;
            OrderRepository = orderRepository;
            OrderProductRepository = orderProductRepository;
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
