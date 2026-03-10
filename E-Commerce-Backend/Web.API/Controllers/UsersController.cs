using Microsoft.AspNetCore.Mvc;
using Web.Service.Users;
using Web.Service.Users.Create;
using Web.Service.Users.Update;

namespace Web.API.Controllers
{
    [Route("api/user")]
    public class UsersController(IUserService userService) : CustomBaseController
    {

        [HttpGet("role/{id:int}")]
        public async Task<IActionResult> GetUsersByRole(int id)
        {
            var userResult = await userService.GetUsersByRole(id);

            return CreateActionResult(userResult);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var userResult = await userService.GetAllListAsync();

            return CreateActionResult(userResult);
        }

        [HttpGet("getbyId/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userResult = await userService.GetByIdAsync(id);

            return CreateActionResult(userResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var userResult = await userService.CreateAsync(request);

            return CreateActionResult(userResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            var userResult = await userService.UpdateAsync(request);

            return CreateActionResult(userResult);
        }


        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userResult = await userService.DeleteAsync(id);

            return CreateActionResult(userResult);
        }
    }
}