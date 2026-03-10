namespace Web.Repository.OrderSection.Order
{
    public interface IOrderRepository : IGenericRepository<OrderModel>
    {
        Task<OrderModel?> GetByIdDetailAsync(int orderId);
        Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<OrderModel>> GetOrdersByStatusAsync(string status);
        Task<IEnumerable<OrderModel>> GetOrdersByDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<OrderModel>> GetOrdersByTotalAmountAsync(decimal totalAmount);
        Task<IEnumerable<OrderModel>> GetOrdersByStatusAndDateAsync(string status, DateTime startDate, DateTime endDate);
        Task<OrderModel?> GetOrderWithPaymentsAsync(int orderId);
    }
}
