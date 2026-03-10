using Microsoft.EntityFrameworkCore;

namespace Web.Repository.UserInfo.Users
{
    public class UserRepository(WebDbContext context) : GenericRepository<UserModel>(context), IUserRepository
    {
        public async Task<List<string>> GetUsersByRole(int id)
        {
            return await Context.Users
                 .Where(x => x.UserId == id)
                 .Select(x => x.UserRole.ToString()) // Enum'ı string'e çeviriyoruz
                 .ToListAsync();
        }

        public async Task<UserModel?> GetUserFindAsync(string email, string password)
        {
            var user = await Context.Users
        .FirstOrDefaultAsync(x => x.UserEmail == email);

            if (user != null && user.UserPassword == password) return user;

            return null;
        }
    }
}