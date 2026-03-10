namespace Web.Service.Reviews.Create;

public record CreateReviewRequest(
    int UserId,
    int ProductId,
    short ReviewRating,
    string ReviewComment = default!
    );  