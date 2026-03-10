using Web.Repository.ProductInfo.Products;
using Web.Repository.UserInfo.Users;

namespace Web.Repository.ProductInfo.Reviews
{
    public class ReviewModel
    {
        public int ReviewsId { get; set; }
        public int UserId { get; set; }
        public UserModel? User { get; set; } // User ile ilişki

        public int ProductId { get; set; }
        public ProductModel? Product { get; set; } // Product ile ilişki

        public short ReviewsRating { get; set; }
        public string ReviewsComment { get; set; } = default!;
        public DateTime ReviewsDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

    }
}
