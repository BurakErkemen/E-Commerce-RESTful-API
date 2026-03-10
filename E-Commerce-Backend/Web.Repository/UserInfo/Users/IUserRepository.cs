namespace Web.Repository.UserInfo.Users
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        public Task<List<string>> GetUsersByRole(int id);

        public Task<UserModel?> GetUserFindAsync(string email, string password);
    }
}