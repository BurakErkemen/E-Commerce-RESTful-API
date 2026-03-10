using Web.Service.UserAddress.Create;
using Web.Service.UserAddress.Update;

namespace Web.Service.UserAddress
{
    public interface IUserAddressService
    {
        Task<ServiceResult<List<AddressResponse>>> GetAllListAsync();
        Task<ServiceResult<AddressResponse?>> GetByIdAsync(int id);
        Task<ServiceResult<List<AddressResponse>>> GetAddressInUserIDAsync(int userId);
        Task<ServiceResult<CreateAddressResponse>> CreateAsync(CreateAddressRequest request);
        Task<ServiceResult> UpdateAsync(UpdateAddressRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
