namespace Web.Service.Reviews;
public record ReviewResponse(
    int ReviewId,
    short ReviewRating,
    string? ReviewComment,
    DateTime ReviewsDateTime,
    string UserFullName,
    string ProdcutStockCode
    );