namespace Web.Repository.UserInfo.UserAddresses
{
    public interface IUserAddressRepository : IGenericRepository<UserAddressModel>
    {
        public Task<List<int>> GetAddressListInUserIDAsync(int userId);
        public Task<List<UserAddressModel>> GetAddressListAsync(List<int> values);
        public Task AddAddressToUserAsync(int userId, int addressId);
    }
}
