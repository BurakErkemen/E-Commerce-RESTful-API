using Microsoft.EntityFrameworkCore;
using System.Net;
using Web.Repository;
using Web.Repository.ProductInfo.Products;
using Web.Service.Products.Create;
using Web.Service.Products.Update;

namespace Web.Service.Products
{
    public class ProductService(
        IProductRepository productRepository, 
        IUnitOFWork unitOFWork) : IProductService
    {
        public async Task<ServiceResult<List<ProductResponse>>> GetTopPriceProductAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);

            var productAsResponse = products.Select(p => new ProductResponse(
                p.ProductID,
                p.ProductName,
                p.ProductDescription,
                p.ProductPrice,
                p.ProductStock,
                p.ProductStockCode,
                p.ProductImg ?? [],
                p.CategoryId,
                p.Reviews
            )).ToList();

            return new ServiceResult<List<ProductResponse>>() { Data = productAsResponse };
        }

        public async Task<ServiceResult<ProductResponse?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductResponse?>.Fail("Product not found!", HttpStatusCode.NotFound);
            }
            #region Manuel Mapper
            var productAsResponse = new ProductResponse(
                product!.ProductID,
                product!.ProductName,
                product!.ProductDescription,
                product!.ProductPrice,
                product!.ProductStock,
                product.ProductStockCode,
                product!.ProductImg ?? [],
                product!.CategoryId,
                product!.Reviews
                );
            #endregion

            return ServiceResult<ProductResponse>.Success(productAsResponse)!;
        }

        public async Task<ServiceResult<List<ProductResponse>>> GetAllListAsync()
        {
            var product = await productRepository.GetAll().ToListAsync();

            #region Manuel Mapper
            var productAsResponse = product.Select(p => new ProductResponse(
                p.ProductID,
                p.ProductName,
                p.ProductDescription,
                p.ProductPrice,
                p.ProductStock,
                p.ProductStockCode,
                p.ProductImg ?? [],
                p.CategoryId,
                p.Reviews
            )).ToList();
            #endregion

            return ServiceResult<List<ProductResponse>>.Success(productAsResponse);
        }

        public async Task<ServiceResult<List<ProductResponse>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            #region Manuel Mapper
            var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var productAsResponse = products.Select(p => new ProductResponse(
                p.ProductID,
                p.ProductName,
                p.ProductDescription,
                p.ProductPrice,
                p.ProductStock,
                p.ProductStockCode,
                p.ProductImg ?? [],
                p.CategoryId,
                p.Reviews
            )).ToList();
            #endregion

            return ServiceResult<List<ProductResponse>>.Success(productAsResponse);
        }


        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            #region async manuel service business check
            var anyProduct = await productRepository.Where(x=> x.ProductStockCode == request.ProductStockCode).AnyAsync();
            if (anyProduct) return ServiceResult<CreateProductResponse>.Fail
                    ("User's email already exists!", HttpStatusCode.BadRequest);
            #endregion

            var product = new ProductModel
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductPrice = request.ProductPrice,
                ProductStock = request.ProductStock,
                ProductStockCode = request.ProductStockCode,
                ProductImg = request.ProductImg,
                CategoryId = request.CategoryId
            };


            await productRepository.AddAsync(product);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(
                product.ProductID),HttpStatusCode.Created);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateProductRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.Id);
            if (product is null) 
                return ServiceResult.Fail("Product not found!", HttpStatusCode.NotFound);

            product.ProductName = request.ProductName;
            product.ProductPrice = request.ProductPrice;
            product.ProductStock = request.ProductStock;
            product.ProductStockCode = request.ProductStockCode;
            product.ProductImg = request.ProductImg;
            product.CategoryId = request.CategoryId;
            product.ProductDescription = request.ProductDescription;

            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);

            if (product is null) 
                return ServiceResult.Fail("Product not found!", HttpStatusCode.NotFound);
            
            product.ProductStock = request.Quantity;

            productRepository.Update(product);
            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null) 
                return ServiceResult.Fail("Product is null", HttpStatusCode.NotFound);

            productRepository.Delete(product);
            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
