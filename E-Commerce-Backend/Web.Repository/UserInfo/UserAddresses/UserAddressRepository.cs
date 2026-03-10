using Microsoft.EntityFrameworkCore;

namespace Web.Repository.UserInfo.UserAddresses
{
    public class UserAddressRepository(WebDbContext context) :
        GenericRepository<UserAddressModel>(context), IUserAddressRepository
    {
        public async Task<List<int>> GetAddressListInUserIDAsync(int userId)
        {

            var user = await Context.Users
            .Where(x => x.UserId == userId)
            .Select(u => u.Addresses!.Select(a => a.AddressId).ToList())
            .FirstOrDefaultAsync();

            return user ?? [];
        }

        public async Task<List<UserAddressModel>> GetAddressListAsync(List<int> values)
        {
            return await Context.UserAddresses
                .Where(x => values.Contains(x.AddressId))
                .ToListAsync();
        }

        public async Task AddAddressToUserAsync(int userId, int addressId)
        {
            var user = await Context.Users
            .Include(u => u.Addresses)
            .FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
                throw new Exception("User not found");

            var address = await Context.UserAddresses.FirstOrDefaultAsync(a => a.AddressId == addressId);

            if (address == null)
                throw new Exception("Address not found");

            user.Addresses!.Add(address);

            Context.Users.Update(user);
            await Context.SaveChangesAsync();
        }
    }
}