using Web.Repository.UserInfo.Users;

namespace Web.Repository.UserInfo.UserAddresses
{
    public class UserAddressModel
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; } = default!; // Description
        public string City { get; set; } = default!; 
        public string Country { get; set; } = default!;
        public string PostCode { get; set; } = default!; // ZipCode
        public string? AddressNote { get; set; }


        // User ile ilişki
        public int UserId { get; set; }
        public UserModel User { get; set; } = default!;
    }
}
