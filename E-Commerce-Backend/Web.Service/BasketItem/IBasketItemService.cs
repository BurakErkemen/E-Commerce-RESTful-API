using Web.Service.BasketItem.Create;

namespace Web.Service.BasketItem
{
    public interface IBasketItemService
    {
        Task<ServiceResult<BasketItemResponse>> GetByIdAsync(int id);
        Task<ServiceResult<CreateBasketItemResponse>> AddItemToBasketAsync(CreateBasketItemRequest request);
        Task<ServiceResult> RemoveItemFromBasketAsync(int basketId);
        Task<ServiceResult<IEnumerable<BasketItemResponse>>> GetItemsByUserIdAsync(int userId);
    }
}
