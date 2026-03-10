using Microsoft.AspNetCore.Mvc;
using Web.Service.Attachments;
using Web.Service.Attachments.Create;
using Web.Service.Attachments.Update;

namespace Web.API.Controllers
{
    [Route("api/attachments")]
    [ApiController]
    public class AttachmentController(IAttachmentService attachmentService) : CustomBaseController
    {

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() =>
            CreateActionResult(await attachmentService.GetAll());

        [HttpGet("getById/{Id:int}")]
        public async Task<IActionResult> GetById(int Id) => 
            CreateActionResult(await attachmentService.GetById(Id));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateAttachmentRequest request) =>
            CreateActionResult(await attachmentService.Create(request));

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateAttachmentRequest request) =>
            CreateActionResult(await attachmentService.Update(request));

        [HttpDelete("delete/{Id:int}")]
        public async Task<IActionResult> Delete(int Id) =>
            CreateActionResult(await attachmentService.Delete(Id));



        [HttpGet("getByTicketId/{ticketId:int}")]
        public async Task<IActionResult> GetByTicketId(int ticketId) =>
            CreateActionResult(await attachmentService.GetAttachmentsByTicketIdAsync(ticketId));

        [HttpGet("getLargeAttachments/{fileSizeThreshold:long}")]
        public async Task<IActionResult> GetLargeAttachments(long fileSizeThreshold) =>
            CreateActionResult(await attachmentService.GetLargeAttachmentsAsync(fileSizeThreshold));

        [HttpGet("searchAttachmentsByFileName/{fileName}")]
        public async Task<IActionResult> SearchAttachmentsByFileName(string fileName) =>
            CreateActionResult(await attachmentService.SearchAttachmentsByFileNameAsync(fileName));

        [HttpGet("getTopNLargestAttachments/{count:int}")]
        public async Task<IActionResult> GetTopNLargestAttachments(int count) =>
            CreateActionResult(await attachmentService.GetTopNLargestAttachmentsAsync(count));

        [HttpGet("getAttachmentCountByTicketId/{ticketId:int}")]
        public async Task<IActionResult> GetAttachmentCountByTicketId(int ticketId) =>
            CreateActionResult(await attachmentService.GetAttachmentCountByTicketIdAsync(ticketId));
    }
}
