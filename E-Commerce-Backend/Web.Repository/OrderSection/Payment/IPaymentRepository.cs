using Iyzipay.Request;
namespace Web.Repository.OrderSection.Payment
{
    public interface IPaymentRepository : IGenericRepository<PaymentModel>
    {
        Task<PaymentModel?> GetByOrderIdAsync(int orderId);
        Task<IEnumerable<PaymentModel>> GetPaymentsByStatusAsync(string status);
        Task<IEnumerable<PaymentModel>> GetPaymentsByDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<PaymentModel>> GetPaymentsByOrderIdAsync(int orderId);
        Task<Iyzipay.Model.Payment> ProcessPaymentAsync(CreatePaymentRequest request);
    }
}
