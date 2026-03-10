namespace Web.Service.BasketItem;
public record BasketItemResponse(
    int BasketItemId,
    int UserId,
    int ProductId,
    string ProductName,
    int Quantity,
    decimal Price
    );
