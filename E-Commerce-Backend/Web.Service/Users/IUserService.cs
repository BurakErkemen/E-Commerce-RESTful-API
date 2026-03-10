using Web.Repository.UserInfo.Users;
using Web.Service.Users.Create;
using Web.Service.Users.Update;

namespace Web.Service.Users;
public interface IUserService
{
    Task<ServiceResult<List<string>>> GetUsersByRole(int id);
    Task<ServiceResult<List<UserResponse>>> GetAllListAsync();
    Task<ServiceResult<UserResponse?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateUserResponse>> CreateAsync(CreateUserRequest request);
    Task<ServiceResult> UpdateAsync(UpdateUserRequest request);
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult<UserModel?>> GetUserFindAsync(string email, string password);
}

