using ECommerceAPI.BLL;
using ECommerceAPI.Common;
using ECommerceAPI.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserOnly")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<GeneralResult<OrderReadDTo>>> GetUserOrderById(int orderId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return GeneralResult<OrderReadDTo>.NotFound();

            var order = await _orderManager.GetOrderByUserId(userId, orderId);
            if (order == null)
                return GeneralResult<OrderReadDTo>.NotFound("Order not found");
            
            return Ok(GeneralResult<OrderReadDTo>.SuccessResult(order,$"Order with id {orderId} Found"));
        }
        [HttpGet]
        public async Task<ActionResult<GeneralResult<List<OrderReadDTo>>>> GetAllUserOrders()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return GeneralResult<List<OrderReadDTo>>.NotFound();

            var order = (await _orderManager.GetOrdersByUserId(userId)).ToList();
            if (!order.Any())
                return GeneralResult<List<OrderReadDTo>>.NotFound("No Orders found");
            
            return Ok(GeneralResult<List<OrderReadDTo>>.SuccessResult(order));
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult<OrderReadDTo>>> GenerateOrder()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return GeneralResult<OrderReadDTo>.NotFound();

            var newOrder = (await _orderManager.GenerateOrder(userId));
            if (newOrder == null)
                return GeneralResult<OrderReadDTo>.FailResult("Something wrong happend");
            
            return Ok(GeneralResult<OrderReadDTo>.SuccessResult(newOrder));
        }
    }
}
