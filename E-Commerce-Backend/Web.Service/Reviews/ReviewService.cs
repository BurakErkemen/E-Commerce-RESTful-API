using Microsoft.EntityFrameworkCore;
using System.Net;
using Web.Repository;
using Web.Repository.ProductInfo.Products;
using Web.Repository.ProductInfo.Reviews;
using Web.Repository.UserInfo.Users;
using Web.Service.Reviews.Create;
using Web.Service.Reviews.Update;

namespace Web.Service.Reviews
{
    public class ReviewService(
        IReviewRepository reviewRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        IUnitOFWork unitOFWork) : IReviewService
    {

        public async Task<ServiceResult<CreateReviewResponse>> CreateReviewAsync(CreateReviewRequest request)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                return ServiceResult<CreateReviewResponse>.Fail("User not found!", HttpStatusCode.NotFound);

            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
                return ServiceResult<CreateReviewResponse>.Fail("Product not found!", HttpStatusCode.NotFound);

            var review = new ReviewModel
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                ReviewsRating = request.ReviewRating,
                ReviewsComment = request.ReviewComment,
                ReviewsDateTime = DateTime.Now,
            };

            await reviewRepository.AddAsync(review);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateReviewResponse>.Success(new CreateReviewResponse(review.ReviewsId),
                HttpStatusCode.Created);
        }

        public async Task<ServiceResult> UpdateReviewAsync(UpdateReviewRequest request)
        {
            var review = await reviewRepository.GetByIdAsync(request.reviewId);

            if (review is null)
                return ServiceResult.Fail("Review not found", HttpStatusCode.NotFound);


            review.ReviewsRating = request.ReviewRating;
            review.ReviewsComment = request.ReviewComment!;
            review.UpdateDateTime = DateTime.Now;

            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteReviewAsync(int reviewId)
        {
            var review = await reviewRepository.GetByIdAsync(reviewId);

            if (review is null)
                return ServiceResult.Fail("Review not found", HttpStatusCode.NotFound);

            reviewRepository.Delete(review);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


        public async Task<ServiceResult<List<ReviewResponse>>> GetAllAsync()
        {
            var review = await reviewRepository.GetAll().ToListAsync();
            if (review is null)
                return ServiceResult<List<ReviewResponse>>.Fail("Reviews not found!", HttpStatusCode.NotFound);

            var reviewAsResponse = review.Select(r => new ReviewResponse(
                r.ReviewsId,
                r.ReviewsRating,
                r.ReviewsComment,
                r.ReviewsDateTime,
                r.User != null ? $"{r.User.UserName} {r.User.UserLastName}" : "Unknown User",
                r.Product?.ProductStockCode ?? "Unknown Stock Code"
                )).ToList();
            return ServiceResult<List<ReviewResponse>>.Success(reviewAsResponse);
        }
        public async Task<ServiceResult<ReviewResponse>> GetReviewByIdAsync(int reviewId)
        {
            var review = await reviewRepository.GetReviewByIdWithDetailsAsync(reviewId);

            if (review is null)
                return ServiceResult<ReviewResponse>.Fail("Review Not Found!", HttpStatusCode.NotFound);

            var reviewAsResponse = new ReviewResponse(
                review.ReviewsId,
                review.ReviewsRating,
                review.ReviewsComment,
                review.ReviewsDateTime,
                review.User != null ? $"{review.User.UserName} {review.User.UserLastName}" : "Unknown User",
                review.Product?.ProductStockCode ?? "Unknown Stock Code"
                );

            return ServiceResult<ReviewResponse>.Success(reviewAsResponse);
        }
        public async Task<ServiceResult<double>> GetAverageRatingByProductIdAsync(int productId)
        {
            var avarageRating = await reviewRepository.GetAverageRatingByProductIdAsync(productId);

            return ServiceResult<double>.Success(avarageRating);
        }
        public async Task<ServiceResult<List<ReviewResponse>>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await reviewRepository.GetReviewsByProductIdAsync(productId);
            var response = reviews
                .Select(r => new ReviewResponse
                (r.ReviewsId,
                r.ReviewsRating,
                r.ReviewsComment,
                r.ReviewsDateTime,
                r.User != null ? $"{r.User.UserName} {r.User.UserLastName}" : "Unknown User",
                r.Product?.ProductStockCode ?? "Unknown Stock Code"
                )).ToList();

            return ServiceResult<List<ReviewResponse>>.Success(response);
        }
        public async Task<ServiceResult<List<ReviewResponse>>> GetReviewsByUserIdAsync(int userId)
        {
            var userReviews = await reviewRepository.GetReviewsByUserIdAsync(userId);
            var response = userReviews.Select(r => new ReviewResponse(
                r.ReviewsId,
                r.ReviewsRating,
                r.ReviewsComment,
                r.ReviewsDateTime,
                r.User != null ? $"{r.User.UserName} {r.User.UserLastName}" : "Unknown User",
                r.Product?.ProductStockCode ?? "Unknown Stock Code"
                )).ToList();

            return ServiceResult<List<ReviewResponse>>.Success(response);
        }
    }
}
