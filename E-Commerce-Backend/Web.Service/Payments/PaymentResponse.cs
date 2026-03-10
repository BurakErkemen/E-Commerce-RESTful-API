using Web.Repository.OrderSection.Payment;

namespace Web.Service.Payments;
public record PaymentResponse(
    int PaymentId,
    int OrderId,
    decimal Amount,
    string PaymentStatus,
    string PaymentMethod,
    string TransactionId,
    DateTime CreatedDate
    );