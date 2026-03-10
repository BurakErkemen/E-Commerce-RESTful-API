using Microsoft.EntityFrameworkCore;

namespace Web.Repository.ProductInfo.Products
{
    public class ProductRepository(WebDbContext context) : GenericRepository<ProductModel>(context), IProductRepository
    {
        public async Task<List<ProductModel>> GetTopPriceProductAsync(int count)
        {
            return await Context.Products.OrderByDescending(x => x.ProductPrice).Take(count).ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await Context.Categories
                .AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}
