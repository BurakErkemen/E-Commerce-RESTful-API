using Microsoft.EntityFrameworkCore;
using System.Net;
using Web.Repository;
using Web.Repository.UserInfo.Users;
using Web.Service.Users.Create;
using Web.Service.Users.Update;

namespace Web.Service.Users
{
    public class UserService(IUserRepository userRepository, IUnitOFWork unitOFWork) : IUserService
    {
        public async Task<ServiceResult<List<string>>> GetUsersByRole(int id)
        {
            var user = await userRepository.GetUsersByRole(id);

            return new ServiceResult<List<string>>() { Data = user };
        }

        public async Task<ServiceResult<List<UserResponse>>> GetAllListAsync()
        {
            var user = await userRepository.GetAll().ToListAsync();

            var userAsResponse = user.Select(x => new UserResponse(
                x.UserName,
                x.UserLastName,
                x.UserEmail,
                x.UserPassword,
                x.UserPhoneNumber,
                x.UserDateOfBirth,
                x.UserRole.ToString(),
                x.Addresses?.Select(x => x.AddressId).ToList(),
                x.MarketingConsent
            )).ToList();

            return ServiceResult<List<UserResponse>>.Success(userAsResponse);
        }

        public async Task<ServiceResult<UserResponse?>> GetByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return ServiceResult<UserResponse?>.Fail("User not found", HttpStatusCode.NotFound);
            }

            var userAsResponse = new UserResponse(
                user.UserName,
                user.UserLastName,
                user.UserEmail,
                user.UserPassword,
                user.UserPhoneNumber,
                user.UserDateOfBirth,
                user.UserRole.ToString(),
                user.Addresses?.Select(a => a.AddressId).ToList(),
                user.MarketingConsent
            );

            return ServiceResult<UserResponse>.Success(userAsResponse)!;
        }

        public async Task<ServiceResult<CreateUserResponse>> CreateAsync(CreateUserRequest request)
        {
            #region async manuel service business check
            var anyUser = await userRepository.Where(x => x.UserEmail == request.UserEmail).AnyAsync();

            if (anyUser) return ServiceResult<CreateUserResponse>.Fail
                    ("User's email already exists!", HttpStatusCode.BadRequest);
            #endregion
            var user = new UserModel
            {
                UserName = request.UserName,
                UserLastName = request.UserLastName,
                UserEmail = request.UserEmail,
                UserPassword = request.UserPassword,
                UserPhoneNumber = request.UserPhoneNumber,
                UserDateOfBirth = request.UserDateOfBirth,
                CreateDate = DateTime.Now,
                MarketingConsent = request.MarketingConsent
            };

            await userRepository.AddAsync(user);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateUserResponse>.Success(new CreateUserResponse(user.UserId),
                HttpStatusCode.Created);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateUserRequest request)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                return ServiceResult.Fail("User not found", HttpStatusCode.NotFound);
            }
            var DateOfBirth = request.UserDateOfBirth.ToString("dd/MM/yyyy HH:mm");

            user.UserName = request.UserName;
            user.UserLastName = request.UserLastName;
            user.UserEmail = request.UserEmail;
            user.UserPassword = request.UserPassword;
            user.UserPhoneNumber = request.UserPhoneNumber;
            user.UserDateOfBirth = DateTime.Parse(DateOfBirth);
            user.MarketingConsent = request.MarketingConsent;

            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return ServiceResult.Fail("User not found", HttpStatusCode.NotFound);
            }

            userRepository.Delete(user);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<UserModel?>> GetUserFindAsync(string email, string password)
        {
            var user = await userRepository.GetUserFindAsync(email, password);
            if (user is null)
            {
                return ServiceResult<UserModel?>.Fail("User email or password not found", HttpStatusCode.NotFound);
            }
            if (user.UserPassword != password)
            {
                return ServiceResult<UserModel?>.Fail("Password not found", HttpStatusCode.NoContent);
            }
            return ServiceResult<UserModel?>.Success(user); // User? olarak dönüyoruz
        }

    }
}
