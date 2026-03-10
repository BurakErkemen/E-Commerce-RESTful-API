using Web.Service.Products.Create;
using Web.Service.Products.Update;

namespace Web.Service.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductResponse>>> GetAllListAsync();
        Task<ServiceResult<ProductResponse?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateAsync(UpdateProductRequest request);
        Task<ServiceResult> DeleteAsync(int id);

        Task<ServiceResult<List<ProductResponse>>> GetTopPriceProductAsync(int count);
        Task<ServiceResult<List<ProductResponse>>> GetPagedAllListAsync(int pageNumber, int pageSize);
        Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
    }
}
