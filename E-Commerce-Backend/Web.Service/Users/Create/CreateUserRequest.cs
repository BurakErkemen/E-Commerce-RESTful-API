namespace Web.Service.Users.Create;
public record CreateUserRequest(
string UserName,
string UserLastName,
string UserEmail,
string UserPassword,
string UserPhoneNumber,
DateTime UserDateOfBirth,
DateTime CreateDate,
bool MarketingConsent
    );