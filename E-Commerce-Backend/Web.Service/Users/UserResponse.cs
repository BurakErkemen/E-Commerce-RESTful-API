namespace Web.Service.Users;
public record UserResponse(
    string UserName,
    string UserLastName,
    string UserEmail,
    string UserPassword,
    string UserPhoneNumber,
    DateTime UserDateOfBirth,
    string UserRole,
    List<int>? Addresses,
    bool MarketingConsent
    );
