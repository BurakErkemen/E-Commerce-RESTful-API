using Web.Repository.OrderSection.Order;

namespace Web.Repository.OrderSection.Payment
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } = string.Empty; // Önerilen alan

        // Iyzico ile ödeme yapılan ödeme türü (Örn: kredi kartı, banka transferi vs.)
        public string PaymentMethod { get; set; } = string.Empty;

        // Ödeme işlemi sırasında alınan işlem ID'si
        public string TransactionId { get; set; } = string.Empty;

        // İlişkiler
        public virtual OrderModel Order { get; set; } = default!; // Ödeme, siparişle ilişkilendirilecek
    }
}