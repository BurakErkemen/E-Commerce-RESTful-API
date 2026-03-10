namespace Web.Service.Orders.Create;
public record CreateOrderRequest(
    int UserId,
    decimal TotalAmount
    );