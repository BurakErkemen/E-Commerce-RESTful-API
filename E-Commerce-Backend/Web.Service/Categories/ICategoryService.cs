using Web.Service.Categories.Create;
using Web.Service.Categories.Update;

namespace Web.Service.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<List<CategoryResponse>>> GetAllAsync();
        Task<ServiceResult<CategoryResponse>> GetByIdAsync(int id);
        Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
