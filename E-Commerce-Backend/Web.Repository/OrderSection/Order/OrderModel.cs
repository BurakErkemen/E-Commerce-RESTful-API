using Web.Repository.OrderSection.BasketItem;
using Web.Repository.OrderSection.Payment;
using Web.Repository.UserInfo.Users;

namespace Web.Repository.OrderSection.Order
{
    public class OrderModel
    {
        // Primary key for the order
        public int OrderId { get; set; }

        // Foreign key for the user who placed the order
        public int UserId { get; set; }

        // Total amount for the order
        public decimal TotalAmount { get; set; }

        // Date and time when the order was placed
        public DateTime OrderDate { get; set; }

        // Status of the order (e.g., Pending, Completed, Cancelled)
        public string Status { get; set; } = string.Empty;


        // Navigation properties
        // User who placed the order
        public virtual UserModel? User { get; set; }

        // Collection of basket items associated with the order
        public virtual ICollection<BasketItemModel> BasketItems { get; set; } = [];

        // Collection of payments associated with the order
        public virtual ICollection<PaymentModel> Payments { get; set; } = [];
    }
}