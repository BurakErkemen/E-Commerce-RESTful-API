namespace Web.Repository.ProductInfo.Reviews
{
    public interface IReviewRepository : IGenericRepository<ReviewModel>
    {
        Task<List<ReviewModel>> GetReviewsByUserIdAsync(int userId);
        Task<List<ReviewModel>> GetReviewsByProductIdAsync(int productId);
        Task<double> GetAverageRatingByProductIdAsync(int productId);
        Task<ReviewModel?> GetReviewByIdWithDetailsAsync(int reviewId);
    }
}
