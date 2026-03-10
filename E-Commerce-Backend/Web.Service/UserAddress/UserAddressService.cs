using Microsoft.EntityFrameworkCore;
using System.Net;
using Web.Repository;
using Web.Repository.UserInfo.UserAddresses;
using Web.Service.UserAddress.Create;
using Web.Service.UserAddress.Update;

namespace Web.Service.UserAddress
{
    public class UserAddressService
        (IUserAddressRepository addressRepository, 
        IUnitOFWork unitOFWork) : IUserAddressService
    {
        public async Task<ServiceResult<List<AddressResponse>>> GetAllListAsync()
        {
            var address = await addressRepository.GetAll().ToListAsync();
            var addressAsResponse = address.Select(x => new AddressResponse(
                x.AddressLine,
                x.City,
                x.Country,
                x.PostCode,
                x.AddressNote,
                x.UserId
            )).ToList();

            return ServiceResult<List<AddressResponse>>.Success(addressAsResponse);
        }
        public async Task<ServiceResult<AddressResponse?>> GetByIdAsync(int id)
        {
            var address = await addressRepository.GetByIdAsync(id);
            if (address is null)
            {
                return ServiceResult<AddressResponse?>.Fail("Address not found", HttpStatusCode.NotFound);
            }
            var addressAsResponse = new AddressResponse(
                address.AddressLine,
                address.City,
                address.Country,
                address.PostCode,
                address.AddressNote,
                address.UserId
            );
            return ServiceResult<AddressResponse>.Success(addressAsResponse)!;
        }
        public async Task<ServiceResult<List<AddressResponse>>> GetAddressInUserIDAsync(int userId)
        {
            var addressList = await addressRepository.GetAddressListInUserIDAsync(userId);
            if (addressList.Count == 0 && addressList == null)
            {
                return ServiceResult<List<AddressResponse>>.Fail("Address is not found!", HttpStatusCode.BadRequest);
            }

            var address = await addressRepository.GetAddressListAsync(addressList);
            var addressAsResponse = address.Select(x => new AddressResponse(
                x.AddressLine,
                x.City,
                x.Country,
                x.PostCode,
                x.AddressNote,
                x.UserId
            )).ToList();
            return ServiceResult<List<AddressResponse>>.Success(addressAsResponse)!;
        }

        public async Task<ServiceResult<CreateAddressResponse>> CreateAsync(CreateAddressRequest request)
        {
            var address = new UserAddressModel
            {
                AddressLine = request.AddressLine,
                City = request.City,
                Country = request.Country,
                PostCode = request.PostCode,
                AddressNote = request.AddressNote,
                UserId = request.UserId
            };
            await addressRepository.AddAsync(address);
            await unitOFWork.SaveChangesAsync();

            await addressRepository.AddAddressToUserAsync(request.UserId, address.AddressId);
            await unitOFWork.SaveChangesAsync();
            return ServiceResult<CreateAddressResponse>.Success(new CreateAddressResponse(address.AddressId),
                HttpStatusCode.Created);
        }
        public async Task<ServiceResult> UpdateAsync(UpdateAddressRequest request)
        {
            var Address = await addressRepository.GetByIdAsync(request.AddressId);

            if (Address is null)
            {
                return ServiceResult.Fail("Address not found", HttpStatusCode.NotFound);
            }
            
            Address.AddressLine = request.AddressLine;
            Address.City = request.City;
            Address.Country = request.Country;
            Address.PostCode = request.PostCode;
            Address.AddressNote = request.AddressNote;


            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var Address = await addressRepository.GetByIdAsync(id);

            if (Address is null)
            {
                return ServiceResult.Fail("Address not found", HttpStatusCode.NotFound);
            }

            addressRepository.Delete(Address);
            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
