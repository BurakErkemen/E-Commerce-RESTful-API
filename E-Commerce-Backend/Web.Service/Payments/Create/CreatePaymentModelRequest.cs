namespace Web.Service.Payments.Create;
public record CreatePaymentModelRequest(
    int OrderId,
    decimal Amount,
    string PaymentStatus,
    string PaymentMethod,
    string TransactionId
    );