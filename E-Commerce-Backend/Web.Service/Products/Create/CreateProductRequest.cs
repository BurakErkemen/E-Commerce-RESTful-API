namespace Web.Service.Products.Create;
public record CreateProductRequest
(
    string ProductName,
    string ProductDescription,
    decimal ProductPrice,
    int ProductStock, 
    string ProductStockCode, // Unique
    List<string>? ProductImg,
    int CategoryId
);