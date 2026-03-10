namespace Web.Service.Products.Update;
public record UpdateProductRequest(
    int Id,
    string ProductName,
    string ProductDescription,
    decimal ProductPrice,
    int ProductStock,
    string ProductStockCode,
    List<string>? ProductImg,
    int CategoryId
    );