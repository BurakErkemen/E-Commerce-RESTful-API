namespace Web.Service.Notifications;
public record NotificationResponse(
    string UserEmail,
    string Message,
    DateTime MessageDate
    );