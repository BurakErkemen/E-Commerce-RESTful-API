using Microsoft.AspNetCore.Mvc;
using Web.Service.Reviews;
using Web.Service.Reviews.Create;
using Web.Service.Reviews.Update;

namespace Web.API.Controllers
{
    [Route("api/reviews")]
    
    public class ReviewController(IReviewService reviewService) : CustomBaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateReviewRequest request){
            var result = await reviewService.CreateReviewAsync(request);

            return CreateActionResult(result);
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update (UpdateReviewRequest request)
        {
            var result = await reviewService.UpdateReviewAsync(request);

            return CreateActionResult(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete (int reviewID)
        {
            var result = await reviewService.DeleteReviewAsync(reviewID);

            return CreateActionResult(result);
        }

        [HttpGet("getbyId")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            var result = await reviewService.GetReviewByIdAsync(reviewId);

            return CreateActionResult(result);
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await reviewService.GetAllAsync();

            return CreateActionResult(result);
        }

        [HttpGet("avarageRating/{id:int}")]
        public async Task<IActionResult> GetAverageRatingByProduct(int id)
        {
            var result = await reviewService.GetAverageRatingByProductIdAsync(id);

            return CreateActionResult(result);
        }
        [HttpGet("productReviews/{id:int}")]
        public async Task<IActionResult> GetReviewsByProduct(int id)
        {
            var result = await reviewService.GetReviewsByProductIdAsync(id);

            return CreateActionResult(result);
        }

        [HttpGet("userReviews/{id:int}")]
        public async Task<IActionResult> GetReviewsByUser(int id)
        {
            var result = await reviewService.GetReviewsByUserIdAsync(id);

            return CreateActionResult(result);
        }
    }
}
