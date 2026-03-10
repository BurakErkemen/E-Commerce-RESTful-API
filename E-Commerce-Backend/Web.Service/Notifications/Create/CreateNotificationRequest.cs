namespace Web.Service.Notifications.Create;
public record CreateNotificationRequest(
    string UserEmail,
    string Message
    );