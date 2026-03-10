using Microsoft.AspNetCore.Mvc;
using Web.Service.BasketItem;
using Web.Service.BasketItem.Create;

namespace Web.API.Controllers
{
    [Route("api/basketItem")]
    [ApiController]
    public class BasketItemController(IBasketItemService basketItemService) : CustomBaseController
    {
        // Get by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await basketItemService.GetByIdAsync(id);
            return CreateActionResult(result);
        }

        // Add item to basket
        [HttpPost]
        public async Task<IActionResult> AddItemToBasketAsync([FromBody] CreateBasketItemRequest request)
        {
            var result = await basketItemService.AddItemToBasketAsync(request);
            return CreateActionResult(result);
        }

        // Remove item from basket
        [HttpDelete("{basketId}")]
        public async Task<IActionResult> RemoveItemFromBasketAsync(int basketId)
        {
            var result = await basketItemService.RemoveItemFromBasketAsync(basketId);
            return CreateActionResult(result);
        }

        // Get all items by user Id
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetItemsByUserIdAsync(int userId)
        {
            var result = await basketItemService.GetItemsByUserIdAsync(userId);
            return CreateActionResult(result);
        }
    }
}
