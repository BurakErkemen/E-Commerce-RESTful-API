using Web.Repository.OrderSection.Order;

namespace Web.Repository.OrderSection.BasketItem
{
    public interface IBasketItemRepository : IGenericRepository<BasketItemModel>
    {
        Task<IEnumerable<BasketItemModel>> GetBasketItemsByUserIdAsync(int userId);
        Task AddToBasketAsync(BasketItemModel basketItem);
        Task RemoveFromBasketAsync(int basketItemId);
        Task ClearBasketAsync(int userId);
        Task<BasketItemModel?> GetByIdDetailAsync(int id);
    }
}