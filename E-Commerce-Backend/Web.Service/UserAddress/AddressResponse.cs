namespace Web.Service.UserAddress;
public record AddressResponse
(
    string AddressLine,
    string City,
    string Country,
    string PostalCode,
    string? AddressNote,
    int UserId
    );