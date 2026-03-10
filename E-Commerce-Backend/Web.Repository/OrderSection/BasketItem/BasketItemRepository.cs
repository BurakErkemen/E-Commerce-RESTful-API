using Microsoft.EntityFrameworkCore;

namespace Web.Repository.OrderSection.BasketItem
{
    public class BasketItemRepository(WebDbContext context) : GenericRepository<BasketItemModel>(context), IBasketItemRepository
    {
        public async Task<IEnumerable<BasketItemModel>> GetBasketItemsByUserIdAsync(int userId)
        {
            return await Context.BasketItems
                .Include(b => b.Product) // Ürün bilgileriyle birlikte çek
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
        public async Task ClearBasketAsync(int userId)
        {
            var basketItems = await GetBasketItemsByUserIdAsync(userId);
            Context.BasketItems.RemoveRange(basketItems);
            await Context.SaveChangesAsync();
        }

        public async Task AddToBasketAsync(BasketItemModel basketItem)
        {
            Context.BasketItems.Add(basketItem);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveFromBasketAsync(int basketItemId)
        {
            var item = await Context.BasketItems.FindAsync(basketItemId);
            if (item != null)
            {
                Context.BasketItems.Remove(item);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<BasketItemModel?> GetByIdDetailAsync(int id)
        {
            return await Context.BasketItems
                .Include(b => b.Product)
                .FirstOrDefaultAsync(b => b.BasketItemId == id);
        }
    }
}