using Web.Repository.ProductInfo.Reviews;

namespace Web.Service.Products;

public record ProductResponse(
    int Id,
    string ProductName,
    string ProductDescription,
    decimal ProductPrice,
    int ProductStock,
    string ProductStockCode,
    List<string> ProductImg,
    int CategoryId,
    List<ReviewModel>? Reviews
    );