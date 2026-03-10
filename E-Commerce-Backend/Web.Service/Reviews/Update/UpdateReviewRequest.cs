namespace Web.Service.Reviews.Update;
public record UpdateReviewRequest(
    int reviewId,
    short ReviewRating,
    string? ReviewComment
    );