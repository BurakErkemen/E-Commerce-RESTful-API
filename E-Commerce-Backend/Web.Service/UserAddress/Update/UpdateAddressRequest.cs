namespace Web.Service.UserAddress.Update;
public record UpdateAddressRequest
    (
    int AddressId,
    string AddressLine,
    string City,
    string Country,
    string PostCode,
    string AddressNote,
    int UserId
    );