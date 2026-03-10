using Web.Repository.UserInfo.Users;

namespace Web.Repository.Admin.Notifications
{
    public class NotificationModel
    {
        public int NotificationId { get; set; }
        public string UserEmail { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime MessageDate { get; set; }

        // Navigation properties
        public virtual UserModel? Users { get; set; }
    }
}
