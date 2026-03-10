using Microsoft.AspNetCore.Mvc;
using Web.Service.UserAddress;
using Web.Service.UserAddress.Create;
using Web.Service.UserAddress.Update;

namespace Web.API.Controllers
{
    [Route("api/address")]
    public class UserAddressController (IUserAddressService addressService) : CustomBaseController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var addressResult = await addressService.GetAllListAsync();

            return CreateActionResult(addressResult);
        }

        [HttpGet("getbyId/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var addressResult = await addressService.GetByIdAsync(id);

            return CreateActionResult(addressResult);
        }

        [HttpGet("getuseraddresses/{userId:int}")]
        public async Task<IActionResult> GetAddressInUserID(int userId)
        {
            var addressResult = await addressService.GetAddressInUserIDAsync(userId);

            return CreateActionResult(addressResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateAddressRequest request)
        {
            var addressResult = await addressService.CreateAsync(request);

            return CreateActionResult(addressResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateAddressRequest request)
        {
            var addressResult = await addressService.UpdateAsync(request);

            return CreateActionResult(addressResult);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var addressResult = await addressService.DeleteAsync(id);

            return CreateActionResult(addressResult);
        }

    }
}
