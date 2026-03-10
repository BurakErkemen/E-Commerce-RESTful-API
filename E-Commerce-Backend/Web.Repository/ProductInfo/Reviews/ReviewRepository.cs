using Microsoft.EntityFrameworkCore;

namespace Web.Repository.ProductInfo.Reviews
{
    public class ReviewRepository(WebDbContext context) : GenericRepository<ReviewModel>(context), IReviewRepository
    {
        public async Task<List<ReviewModel>> GetReviewsByUserIdAsync(int userId)
        {
            return await Context.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.User)       // Kullanıcı bilgilerini dahil et
                .Include(r => r.Product)    // Ürün bilgilerini dahil et
                .ToListAsync();
        }

        public async Task<List<ReviewModel>> GetReviewsByProductIdAsync(int productId)
        {
            return await Context.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)       // Kullanıcı bilgilerini dahil et
                .Include(r => r.Product)    // Ürün bilgilerini dahil et
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingByProductIdAsync(int productId)
        {
            var reviews = await Context.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)       // Kullanıcı bilgilerini dahil et
                .Include(r => r.Product)    // Ürün bilgilerini dahil et
                .ToListAsync();

            return reviews.Count != 0
                ? reviews.Average(r => (double)r.ReviewsRating)
                : 0.0; // Eğer değerlendirme yoksa 0 döndür.
        }

        public async Task<ReviewModel?> GetReviewByIdWithDetailsAsync(int reviewId)
        {
            return await Context.Reviews
                .Include(r => r.User)       // Kullanıcı bilgilerini dahil et
                .Include(r => r.Product)    // Ürün bilgilerini dahil et
                .FirstOrDefaultAsync(r => r.ReviewsId == reviewId);
        }
    }
}
