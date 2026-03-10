namespace Web.Service.Orders;
public record OrderResponse(
       int OrderId,
       int UserId,
       decimal TotalAmount,
       string Status,
       DateTime CreatedDate
   );