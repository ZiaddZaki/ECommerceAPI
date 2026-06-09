using ECommerceAPI.DAL;

namespace ECommerceAPI.BLL
{
    public class OrderManager : IOrderManager
    {
        private IUnitOfWork _unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderReadDTo>> GetOrdersByUserId(string userId)
        {

            var userOrders = await _unitOfWork.OrderRepository.GetOrdersByUserId(userId);

            var ordersReadDTOs = userOrders.Select(uo => new OrderReadDTo
            {
                Id = uo!.Id,
                TotalPrice = uo.TotalPrice,
                OrderDate = uo.OrderDate,
                Items = uo.OrderProducts.Select(op => new OrderItemDTo
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product!.Name,
                    ProductPrice = op.Product.Price,
                    Quantity = op.Quantity

                }).ToList()
            }).ToList();


            return ordersReadDTOs;
        }
        public async Task<OrderReadDTo?> GetOrderByUserId(string userId, int orderId)
        {

            var userOrder = await _unitOfWork.OrderRepository.GetOrderByUserId(userId, orderId);

            if (userOrder == null)
                return null;

            var orderReadDTo = new OrderReadDTo
            {
                Id = userOrder.Id,
                TotalPrice = userOrder.TotalPrice,
                OrderDate = userOrder.OrderDate,
                Items = userOrder.OrderProducts.Select(op => new OrderItemDTo
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product!.Name,
                    ProductPrice = op.Product.Price
                }).ToList()
            };


            return orderReadDTo;
        }


        public async Task<OrderReadDTo?> GenerateOrder(string userId)
        {
            var userCart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (userCart == null || !userCart.CartProducts.Any())
                return null;

            var cartItems = userCart.CartProducts.ToList();

            Order newOrder = new Order
            {
                UserId = userId,

                OrderProducts = userCart.CartProducts.Select(cp => new OrderProduct
                {
                    ProductId = cp.ProductId,
                    Quantity = cp.Quantity,

                }).ToList(),

                TotalPrice = userCart.CartProducts.Sum(cp => cp.Product!.Price * cp.Quantity),

                OrderDate = DateTime.UtcNow,
            };

            _unitOfWork.OrderRepository.Add(newOrder);

            foreach (var item in userCart.CartProducts)
            {
                _unitOfWork.CartProductRepository.Delete(item);
            }
            await _unitOfWork.SaveChangesAsync();

            var orderReadDTo = new OrderReadDTo
            {
                Id = newOrder.Id,
                TotalPrice = newOrder.TotalPrice,
                OrderDate = newOrder.OrderDate,
                Items = cartItems.Select(cp => new OrderItemDTo
                {
                    ProductId = cp.ProductId,
                    ProductName = cp.Product!.Name,
                    ProductPrice = cp.Product.Price,
                    Quantity = cp.Quantity

                }).ToList()
            };

            return orderReadDTo;
        }
    }
}
