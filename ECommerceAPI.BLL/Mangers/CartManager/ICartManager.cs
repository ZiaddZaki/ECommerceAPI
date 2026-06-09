using ECommerceAPI.BLL.DTOs.CartDTOs;
using ECommerceAPI.DAL;

namespace ECommerceAPI.BLL
{
    public interface ICartManager
    {
        Task<bool> AddProductToCart(AddToCartDTO addToCartDTO, string userId);
        Task<CartReadDTo?> GetUserCart(string userId);
        Task<bool> RemoveProductFromCart(DeleteFromCartDTO deleteFromCartDTO, string userId);
    }
}