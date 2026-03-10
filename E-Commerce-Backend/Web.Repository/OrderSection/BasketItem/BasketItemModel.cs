using Web.Repository.OrderSection.Order;
using Web.Repository.ProductInfo.Products;
using Web.Repository.UserInfo.Users;

namespace Web.Repository.OrderSection.BasketItem
{
    public class BasketItemModel
    {
        public int BasketItemId { get; set; }
        public int UserId { get; set; } // Kullanıcıya ait sepet
        public int ProductId { get; set; } // Sepetteki ürün
        public int Quantity { get; set; } // Ürün adedi
        public decimal Price { get; set; } // Ürün fiyatı
        public decimal UnitPrice { get; set; } // Birim fiyat (sipariş oluşturulurken gerekli)

        // Navigation properties
        public virtual UserModel? User { get; set; }
        public virtual ProductModel? Product { get; set; }
        public virtual OrderModel? Order { get; set; } // Sipariş bilgileri
    }
}