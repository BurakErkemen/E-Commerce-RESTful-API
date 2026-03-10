using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Repository.TicketModels.SupportTickets;
using Web.Service.SupportTickets;
using Web.Service.SupportTickets.Create;
using Web.Service.SupportTickets.Update;

namespace Web.API.Controllers
{
    [Route("api/supportTicket")]
    [ApiController]
    public class SupportTicketController(ISupportTicketService supportTicketService) : CustomBaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() =>
            CreateActionResult(await supportTicketService.GetAllTicketsAsync());

        [HttpGet("getById/{ticketId:int}")]
        public async Task<IActionResult> GetById(int ticketId) =>
            CreateActionResult(await supportTicketService.GetByIdAsync(ticketId));

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateSupportTicketRequest request) =>
            CreateActionResult(await supportTicketService.CreateTicketAsync(request));

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int ticketId) =>
            CreateActionResult(await supportTicketService.DeleteTicketAsync(ticketId));


        [HttpGet("getOpenTicket")]
        public async Task<IActionResult> GetOpenTicket() =>
            CreateActionResult(await supportTicketService.GetOpenTicketsAsync());

        [HttpGet("getTicketsByUserId/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId) =>
            CreateActionResult(await supportTicketService.GetTicketsByUserIdAsync(userId));

        [HttpGet("getOpenTicketsByUserId/{userId:int}")]
        public async Task<IActionResult> GetOpenTicketsByUserId(int userId) =>
            CreateActionResult(await supportTicketService.GetOpenTicketsByUserIdAsync(userId));

        [HttpGet("getTicketsByStatus/{status}")]
        public async Task<IActionResult> GetByStatus(SupportTicketStatus status) =>
            CreateActionResult(await supportTicketService.GetTicketsByStatusAsync(status));

        [HttpGet("getTicketsByPriority/{priority}")]
        public async Task<IActionResult> GetByPriority(SupportTicketPriority priority) =>
            CreateActionResult(await supportTicketService.GetTicketsByPriorityAsync(priority));

        [HttpPatch("updateTicketStatus")]
        public async Task<IActionResult> UpdateTicketStatus(UpdateTicketStatusRequest request) =>
            CreateActionResult(await supportTicketService.UpdateTicketStatusAsync(request));
    }
}
