using Web.Repository;
using Web.Service.Categories.Create;
using System.Net;
using Web.Service.Categories.Update;
using Microsoft.EntityFrameworkCore;
using Web.Repository.ProductInfo.Categories;

namespace Web.Service.Categories
{
    public class CategoryService(
        ICategoryRepository categoryRepository, IUnitOFWork unitOFWork) : ICategoryService
    {
        public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request)
        {
            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (anyCategory)
                return ServiceResult<CreateCategoryResponse>.Fail("Category already exists", HttpStatusCode.BadRequest);

            // Yeni kategori oluşturuluyor
            var category = new CategoryModel
            {
                Name = request.Name,
                Description = request.Description,
                ParentCategoryId = request?.ParentCategoryId,
                SubCategoryId = request?.SubCategoryId,
                CreatedDate = DateTime.UtcNow
            };

            // Kategoriyi veritabanına kaydet
            await categoryRepository.AddAsync(category);
            await unitOFWork.SaveChangesAsync();

            if (category.ParentCategoryId != null)
            {
                var parentCategory = await categoryRepository.GetByIdAsync(category.ParentCategoryId.Value);
                if (parentCategory != null)
                {
                    parentCategory.SubCategoryId ??= new List<int>();
                    parentCategory.SubCategoryId.Add(category.CategoryId);
                    parentCategory.UpdatedDate = DateTime.Now;
                    categoryRepository.Update(parentCategory);
                    await unitOFWork.SaveChangesAsync();
                }
            }

            if (category.SubCategoryId != null && category.SubCategoryId.Count != 0)
            {
                foreach (var subCategoryId in category.SubCategoryId)
                {
                    var subCategory = await categoryRepository.GetByIdAsync(subCategoryId);
                    if (subCategory != null)
                    {
                        subCategory.ParentCategoryId = category.CategoryId; 
                        subCategory.UpdatedDate = DateTime.Now;
                        categoryRepository.Update(subCategory);
                        await unitOFWork.SaveChangesAsync();
                    }
                }
            }

            return ServiceResult<CreateCategoryResponse>.Success(new CreateCategoryResponse(category.CategoryId), HttpStatusCode.OK);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequest request)
        {
            // Kategori var mı kontrolü
            var category = await categoryRepository.GetByIdAsync(request.CategoryId);
            if (category is null)
                return ServiceResult.Fail("Category not found");

            // ParentCategoryId değişmişse eski ve yeni parent kategorileri güncelle
            if (category.ParentCategoryId != request.ParentCategoryId)
            {
                // Eski parent
                if (category.ParentCategoryId.HasValue)
                {
                    var oldParentCategory = await categoryRepository.GetByIdAsync(category.ParentCategoryId.Value);
                    if (oldParentCategory != null)
                    {
                        oldParentCategory.SubCategoryId?.Remove(category.CategoryId);
                        oldParentCategory.UpdatedDate = DateTime.Now;
                        categoryRepository.Update(oldParentCategory);
                    }
                }

                // Yeni parent
                if (request.ParentCategoryId.HasValue)
                {
                    var newParentCategory = await categoryRepository.GetByIdAsync(request.ParentCategoryId.Value);
                    if (newParentCategory != null)
                    {
                        newParentCategory.SubCategoryId ??= new List<int>();
                        newParentCategory.SubCategoryId.Add(category.CategoryId);
                        newParentCategory.UpdatedDate = DateTime.Now;
                        categoryRepository.Update(newParentCategory);
                    }
                }
            }

            // Kategori detaylarını güncelle
            category.Name = request.Name;
            category.Description = request.Description;
            category.ParentCategoryId = request.ParentCategoryId;
            category.UpdatedDate = DateTime.Now;

            // Alt kategorileri ekle
            if (request.SubCategoryId != null && request.SubCategoryId.Any())
            {
                category.SubCategoryId ??= new List<int>();
                foreach (var item in request.SubCategoryId)
                {
                    if (!category.SubCategoryId.Contains(item)) // Aynı ID'yi tekrar ekleme
                        category.SubCategoryId.Add(item);
                }
            }

            categoryRepository.Update(category);

            // Alt kategorilerin parent ID'sini güncelle
            if (category.SubCategoryId != null && category.SubCategoryId.Any())
            {
                foreach (var item in category.SubCategoryId)
                {
                    var subCategory = await categoryRepository.GetByIdAsync(item);
                    if (subCategory != null)
                    {
                        subCategory.ParentCategoryId = category.CategoryId;
                        subCategory.UpdatedDate = DateTime.Now;
                        categoryRepository.Update(subCategory);
                    }
                }
            }

            // Tüm değişiklikleri bir kerede kaydet
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }



        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
                return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);

            categoryRepository.Delete(category);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<CategoryResponse>>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();
            if (categories is null)
                return ServiceResult<List<CategoryResponse>>.Fail("Categories not found");


            // Category model'inden CategoryResponse'e dönüşüm
            var categoryResponses = categories.Select(category => new CategoryResponse(
                category.CategoryId,
                category.Name,
                category.Description,
                category.ParentCategoryId,
                category.SubCategoryId?.Cast<int?>().ToList() ?? new List<int?>() // SubCategoryId'yi dönüşüm yapıyoruz
            )).ToList();

            return ServiceResult<List<CategoryResponse>>.Success(categoryResponses);
        }

        public async Task<ServiceResult<CategoryResponse>> GetByIdAsync(int categoryId)
        {
            var category = await categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                return ServiceResult<CategoryResponse>.Fail("Category not found");

            // SubCategoryId'yi List<int> türünden List<int?> türüne dönüştürme
            var subCategoryIds = category.SubCategoryId?.Cast<int?>().ToList() ?? new List<int?>();

            var categoryResponse = new CategoryResponse(
                category.CategoryId,
                category.Name,
                category.Description,
                category.ParentCategoryId,
                subCategoryIds // Tür uyumluluğunu sağladık
            );

            return ServiceResult<CategoryResponse>.Success(categoryResponse);
        }


    }
}