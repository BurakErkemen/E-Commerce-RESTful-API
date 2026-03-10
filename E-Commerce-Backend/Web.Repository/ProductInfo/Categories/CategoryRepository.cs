using Microsoft.EntityFrameworkCore;

namespace Web.Repository.ProductInfo.Categories
{
    public class CategoryRepository(WebDbContext context) : GenericRepository<CategoryModel>(context), ICategoryRepository
    {
        public async Task<bool> IsParentCategoryValidAsync(int parentId)
        {
            return await Context.Categories.AnyAsync(c => c.CategoryId == parentId);
        }

        public async Task<List<CategoryModel>> GetCategoriesByIdsAsync(List<int> categoryIds)
        {
            return await Context.Categories
                .Where(x => categoryIds.Contains(x.CategoryId))
                .ToListAsync();
        }
        public async Task<CategoryModel?> GetCategoryWithSubCategoriesAsync(int categoryId)
        {
            return await Context.Categories
                .Where(c => c.CategoryId == categoryId)
                .Include(c => c.SubCategories) // Alt kategorileri dahil et
                .FirstOrDefaultAsync();
        }
    }
}
