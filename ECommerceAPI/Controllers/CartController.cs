using ECommerceAPI.BLL;
using ECommerceAPI.BLL.DTOs.CartDTOs;
using ECommerceAPI.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserOnly")]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<CartReadDTo>>> GetUserCart()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return GeneralResult<CartReadDTo>.NotFound();

            var userCart = await _cartManager.GetUserCart(userId);

            if (userCart == null)
                return GeneralResult<CartReadDTo>.NotFound("error happend while getting your cart!");

            return GeneralResult<CartReadDTo>.SuccessResult(userCart);
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> AddToCart([FromBody] AddToCartDTO addToCartDTO)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return GeneralResult.NotFound();

            var userCart = await _cartManager.AddProductToCart(addToCartDTO, userId);

            if (!userCart)
                return GeneralResult.FailResult();

            return GeneralResult.SuccessResult("Product added to the cart successfully");
        }
        [HttpDelete("{productId}")]
        public async Task<ActionResult<GeneralResult>> RemoveFromCart([FromRoute] DeleteFromCartDTO deleteFromCartDTO)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return GeneralResult.NotFound();

            var userCart = await _cartManager.RemoveProductFromCart(deleteFromCartDTO, userId);

            if (!userCart)
                return GeneralResult.FailResult();

            return GeneralResult.SuccessResult("Product removed from the cart successfully");
        }
    }
}
