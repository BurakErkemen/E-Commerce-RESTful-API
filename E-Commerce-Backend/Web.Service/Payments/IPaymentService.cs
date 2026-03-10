using Iyzipay.Request;
using Web.Repository.OrderSection.Payment;
using Web.Service.Payments.Create;


namespace Web.Service.Payments
{
    public interface IPaymentService
    {
        Task<ServiceResult<PaymentResponse>> ProcessPaymentAsync(List<int> basketItemId,int userId, CreatePaymentRequest request);

        Task<ServiceResult<PaymentResponse?>> GetPaymentByIdAsync(int paymentId);
        Task<ServiceResult<PaymentResponse?>> GetPaymentByOrderIdAsync(int orderId);
        Task<ServiceResult<IEnumerable<PaymentResponse>>> GetPaymentsByStatusAsync(string status);
        Task<ServiceResult<IEnumerable<PaymentResponse>>> GetPaymentsByDateAsync(DateTime startDate, DateTime endDate);
        Task<ServiceResult<IEnumerable<PaymentResponse>>> GetPaymentsByOrderIdAsync(int orderId);

        Task<ServiceResult<bool>> CreatePaymentAsync(CreatePaymentModelRequest payment);
        Task<ServiceResult<bool>> UpdatePaymentStatusAsync(int paymentId, string status);

    }
}
