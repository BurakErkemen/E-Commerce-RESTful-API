namespace Web.Service.UserAddress.Create;
public record CreateAddressRequest
(
string AddressLine,
string City,
string Country,
string PostCode,
string AddressNote,
int UserId
);

