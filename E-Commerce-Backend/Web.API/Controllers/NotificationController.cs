using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Service.Notifications;
using Web.Service.Notifications.Create;
using Web.Service.Notifications.Update;

namespace Web.API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController(INotificationService notificationService) : CustomBaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
        {
            var result = await notificationService.CreateAsync(request);
            return CreateActionResult(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationRequest request)
        {
            var result = await notificationService.UpdateAsync(request);
            return CreateActionResult(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete ([FromQuery] int Id)
        {
            var result = await notificationService.DeleteAsync(Id);
            return CreateActionResult(result);
        }
        
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await notificationService.GetAllAsync();
            return CreateActionResult(result);
        }
       
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            var result = await notificationService.GetByIdDetailAsync(Id);
            return CreateActionResult(result);
        }
    }
}
