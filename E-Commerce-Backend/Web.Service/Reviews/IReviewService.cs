using Web.Service.Reviews.Create;
using Web.Service.Reviews.Update;

namespace Web.Service.Reviews
{
    public interface IReviewService
    {
        Task<ServiceResult<CreateReviewResponse>> CreateReviewAsync(CreateReviewRequest request);
        Task<ServiceResult> UpdateReviewAsync(UpdateReviewRequest request);
        Task<ServiceResult> DeleteReviewAsync(int reviewId);

        Task<ServiceResult<List<ReviewResponse>>> GetAllAsync();
        Task<ServiceResult<List<ReviewResponse>>> GetReviewsByUserIdAsync(int userId);
        Task<ServiceResult<List<ReviewResponse>>> GetReviewsByProductIdAsync(int productId);
        Task<ServiceResult<double>> GetAverageRatingByProductIdAsync(int productId);
        Task<ServiceResult<ReviewResponse>> GetReviewByIdAsync(int reviewId);
    }
}
