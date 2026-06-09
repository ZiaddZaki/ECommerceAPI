using ECommerceAPI.BLL.DTOs.CartDTOs;
using ECommerceAPI.DAL;

namespace ECommerceAPI.BLL
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CartReadDTo?> GetUserCart(string userId)
        {
            var UserCart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (UserCart == null)
            {
                return null;
            }
            var cartDto = new CartReadDTo
            {
                CartId = UserCart.Id,

                CartItems = UserCart.CartProducts.Select(cp => new CartItemDTO
                {
                    ProductId = cp.ProductId,
                    ProductName = cp.Product!.Name,
                    Price = cp.Product.Price,
                    Quantity = cp.Quantity
                }).ToList(),
            };
            return cartDto;
        }
        public async Task<bool> AddProductToCart(AddToCartDTO addToCartDTO, string userId)
        {
            var UserCart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (UserCart == null)
            {
                return false;
            }

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(addToCartDTO.ProductId);

            if (product == null)
                return false;

            var existingItem = UserCart.CartProducts.FirstOrDefault(cp => cp.ProductId == addToCartDTO.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {

                CartProduct newItem = new CartProduct
                {
                    ProductId = addToCartDTO.ProductId,
                    CartId = UserCart.Id,
                    Quantity = 1
                };
                _unitOfWork.CartProductRepository.Add(newItem);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveProductFromCart(DeleteFromCartDTO deleteFromCartDTO, string userId)
        {
            var UserCart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (UserCart == null)
            {
                return false;
            }

            var existingItem = UserCart.CartProducts.FirstOrDefault(cp => cp.ProductId == deleteFromCartDTO.ProductId);

            if (existingItem == null)
            {
                return false;
            }

            if (existingItem.Quantity > 1)
                existingItem.Quantity--;


            else
                _unitOfWork.CartProductRepository.Delete(existingItem);

            await _unitOfWork.SaveChangesAsync();

            return true;

        }
    }
}
