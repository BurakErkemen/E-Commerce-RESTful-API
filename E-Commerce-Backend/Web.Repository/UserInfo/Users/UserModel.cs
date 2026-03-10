using Web.Repository.Admin.Notifications;
using Web.Repository.OrderSection.BasketItem;
using Web.Repository.OrderSection.Order;
using Web.Repository.ProductInfo.Reviews;
using Web.Repository.TicketModels.SupportTicketMessages;
using Web.Repository.TicketModels.SupportTickets;
using Web.Repository.UserInfo.UserAddresses;

namespace Web.Repository.UserInfo.Users;

public class UserModel
{
    public int UserId { get; set; }
    public string UserName { get; set; } = default!;
    public string UserLastName { get; set; } = default!;
    public string UserEmail { get; set; } = default!;
    public string UserPhoneNumber { get; set; } = default!;
    public string UserPassword { get; set; } = default!;
    public DateTime UserDateOfBirth { get; set; } = default!;
    public UserRoles UserRole { get; set; } = 0; //UserRoles.User
    public DateTime CreateDate { get; set; } = default!;
    public bool MarketingConsent { get; set; } = false;



    // ilişki
    public ICollection<UserAddressModel>? Addresses { get; set; } = [];
    public ICollection<ReviewModel>? Reviews { get; set; } = [];
    public ICollection<SupportTicketModel>? SupportTickets { get; set; } = [];
    public ICollection<SupportTicketMessageModel>? Messages { get; set; } = []; // Mesajlar koleksiyonu
    public ICollection<BasketItemModel> BasketItems { get; set; } = [];
    public ICollection<OrderModel> Orders { get; set; } = [];
    public virtual ICollection<NotificationModel>? Notifications { get; set; }

}
public enum UserRoles
{
    Admin = 1,
    User = 0 // Varsayılan değer
}
