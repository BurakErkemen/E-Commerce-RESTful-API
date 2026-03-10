namespace Web.Repository.ProductInfo.Categories
{
    public interface ICategoryRepository : IGenericRepository<CategoryModel>
    {
        Task<bool> IsParentCategoryValidAsync(int parentId);
        Task<List<CategoryModel>> GetCategoriesByIdsAsync(List<int> categoryIds);
        Task<CategoryModel?> GetCategoryWithSubCategoriesAsync(int categoryId);
    }
}
