using Microsoft.AspNetCore.Mvc;
using Web.Service.SupportTicketMessages;
using Web.Service.SupportTicketMessages.Create;
using Web.Service.SupportTicketMessages.Update;

namespace Web.API.Controllers
{
    [Route("api/supportTicketMessage/")]
    [ApiController]
    public class SupportTicketMessageController (ISupportTicketMessageService supportTicketMessageService): CustomBaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() => 
            CreateActionResult(await supportTicketMessageService.GetAll());

        [HttpGet("getById/{messageId:int}")]
        public async Task<IActionResult> GetById(int messageId) =>
            CreateActionResult(await supportTicketMessageService.GetById(messageId));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateSupportTicketMessageRequest request) =>
            CreateActionResult(await supportTicketMessageService.Create(request));

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateSupportTicketMessageRequest request) =>
            CreateActionResult(await supportTicketMessageService.Update(request));

        [HttpDelete("delete/{messageId:int}")]
        public async Task<IActionResult> Delete(int messageId) =>
            CreateActionResult(await supportTicketMessageService.Delete(messageId));

        [HttpGet("getMessagesByTicketId/{ticketId:int}")]
        public async Task<IActionResult> GetMessagesByTicketId(int ticketId) =>
            CreateActionResult(await supportTicketMessageService.GetMessagesByTicketIdAsync(ticketId));

        [HttpGet("getMessagesBySenderId/{senderId:int}")]
        public async Task<IActionResult> GetMessagesBySenderId(int senderId) =>
            CreateActionResult(await supportTicketMessageService.GetMessagesBySenderIdAsync(senderId));

        [HttpGet("getMessagesAfterDate/{startDate:DateTime}")]
        public async Task<IActionResult> GetMessagesAfterDate(DateTime startDate) =>
            CreateActionResult(await supportTicketMessageService.GetMessagesAfterDateAsync(startDate));

        [HttpGet("getMessagesWithinDateRange/{startDate:DateTime}/{endDate:DateTime}")]
        public async Task<IActionResult> GetMessagesWithinDateRange(DateTime startDate, DateTime endDate) =>
            CreateActionResult(await supportTicketMessageService.GetMessagesWithinDateRangeAsync(startDate, endDate));

        [HttpGet("getMessageCountByTicketId/{ticketId:int}")]
        public async Task<IActionResult> GetMessageCountByTicketId(int ticketId) =>
            CreateActionResult(await supportTicketMessageService.GetMessageCountByTicketIdAsync(ticketId));

        [HttpGet("getMessageCountBySenderId/{senderId:int}")]
        public async Task<IActionResult> GetMessageCountBySenderId(int senderId) =>
            CreateActionResult(await supportTicketMessageService.GetMessageCountBySenderIdAsync(senderId));
    }
}
