namespace Web.Service.Users.Update;
public record UpdateUserRequest(
    int Id,
    string UserName,
    string UserLastName,
    string UserEmail,
    string UserPassword,
    string UserPhoneNumber,
    DateTime UserDateOfBirth,
    bool MarketingConsent);