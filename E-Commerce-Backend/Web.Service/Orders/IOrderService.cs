using Web.Service.Orders.Create;

namespace Web.Service.Orders
{
    public interface IOrderService
    {
        Task<ServiceResult<CreateOrderResponse>> CreateOrderAsync(CreateOrderRequest request);
        Task<ServiceResult<bool>> UpdateOrderAsync(int orderId, string newStatus);
        Task<ServiceResult<bool>> CancelOrderAsync(int orderId);
        Task<ServiceResult<OrderResponse>> GetOrderDetailsAsync(int orderId);
    }
}
