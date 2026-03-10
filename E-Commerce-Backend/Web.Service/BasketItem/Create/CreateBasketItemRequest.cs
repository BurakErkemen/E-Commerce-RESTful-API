namespace Web.Service.BasketItem.Create;
public record CreateBasketItemRequest(int userId, int productId, int quantity);