namespace Web.Repository.ProductInfo.Products
{
    public interface IProductRepository : IGenericRepository<ProductModel>
    {
        Task<List<ProductModel>> GetTopPriceProductAsync(int count);
        Task<bool> CategoryExistsAsync(int categoryId);

    }
}
