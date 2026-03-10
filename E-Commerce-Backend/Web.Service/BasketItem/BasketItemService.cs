using System.Net;
using Web.Repository;
using Web.Repository.OrderSection.BasketItem;
using Web.Repository.ProductInfo.Products;
using Web.Repository.UserInfo.Users;
using Web.Service.BasketItem.Create;

namespace Web.Service.BasketItem
{
    public class BasketItemService(
        IBasketItemRepository basketItemRepository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        IUnitOFWork unitOFWork) : IBasketItemService
    {
        public async Task<ServiceResult<BasketItemResponse>> GetByIdAsync(int id)
        {
            var basketItem = await basketItemRepository.GetByIdDetailAsync(id);
            if (basketItem == null)
                return ServiceResult<BasketItemResponse>.Fail("Basket item not found.");

            var response = new BasketItemResponse
                (
                basketItem.BasketItemId,
                basketItem.UserId,
                basketItem.ProductId,
                basketItem.Product != null ? basketItem.Product.ProductName : "Unknown",
                basketItem.Quantity,
                basketItem.Product != null ? basketItem.Product.ProductPrice : 0
                );

            return ServiceResult<BasketItemResponse>.Success(response);
        }

        // Sepete ürün eklemek
        public async Task<ServiceResult<CreateBasketItemResponse>> AddItemToBasketAsync(CreateBasketItemRequest request)
        {
            var user = await userRepository.GetByIdAsync(request.userId);
            var product = await productRepository.GetByIdAsync(request.productId);

            if (user == null || product == null)
                throw new Exception("User or product not found.");

            var basketItem = new BasketItemModel
            {
                UserId = request.userId,
                ProductId = request.productId,
                Quantity = request.quantity,
                Price = product.ProductPrice // Product model'inden fiyat alınır.
            };

            await basketItemRepository.AddAsync(basketItem);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateBasketItemResponse>.Success(new CreateBasketItemResponse(basketItem.BasketItemId));
        }

        // Sepetten ürün çıkarma
        public async Task<ServiceResult> RemoveItemFromBasketAsync(int basketId)
        {
            var basketItem = await basketItemRepository.GetByIdAsync(basketId);
            if (basketItem == null)
                return ServiceResult.Fail("Basket item not found.");

            basketItemRepository.Delete(basketItem);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        // Sepetteki tüm ürünleri listeleme
        public async Task<ServiceResult<IEnumerable<BasketItemResponse>>> GetItemsByUserIdAsync(int userId)
        {
            var items = await basketItemRepository.GetBasketItemsByUserIdAsync(userId);
            if (items == null)
                return ServiceResult<IEnumerable<BasketItemResponse>>.Fail("Basket items not found.");

            var response = items.Select(i => new BasketItemResponse(
                i.BasketItemId,
                i.UserId,
                i.ProductId,
                i.Product != null ? i.Product.ProductName : "Unknown",
                i.Quantity,
                i.Product != null ? i.Product.ProductPrice : 0));

            return ServiceResult<IEnumerable<BasketItemResponse>>.Success(response);
        }
    }
}
