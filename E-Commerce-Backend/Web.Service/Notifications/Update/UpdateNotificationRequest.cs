namespace Web.Service.Notifications.Update;
public record UpdateNotificationRequest(
    int Id,
    string UserEmail,
    string Message
    );