using Microsoft.AspNetCore.Mvc;
using Web.Service.Orders;
using Web.Service.Orders.Create;

namespace Web.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController(IOrderService orderService): CustomBaseController
    {
        // Create order
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            var result = await orderService.CreateOrderAsync(request);
            return CreateActionResult(result);
        }

        // Update order status
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrderAsync(int orderId, [FromQuery] string newStatus)
        {
            var result = await orderService.UpdateOrderAsync(orderId, newStatus);
            return CreateActionResult(result);
        }

        // Cancel order
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> CancelOrderAsync(int orderId)
        {
            var result = await orderService.CancelOrderAsync(orderId);
            return CreateActionResult(result);
        }

        // Get order details
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailsAsync(int orderId)
        {
            var result = await orderService.GetOrderDetailsAsync(orderId);
            return CreateActionResult(result);
        }
    }
}
