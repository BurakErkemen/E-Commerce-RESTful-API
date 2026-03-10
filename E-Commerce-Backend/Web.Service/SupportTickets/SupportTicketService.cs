using Microsoft.EntityFrameworkCore;
using System.Net;
using Web.Repository;
using Web.Repository.TicketModels.SupportTickets;
using Web.Repository.UserInfo.Users;
using Web.Service.SupportTickets.Create;
using Web.Service.SupportTickets.Update;

namespace Web.Service.SupportTickets
{
    public class SupportTicketService(ISupportTicketRepository supportTicketRepository, 
        IUserRepository userRepository,
        IUnitOFWork unitOFWork) : ISupportTicketService
    {
        public async Task<ServiceResult<List<SupportTicketResponse>>> GetAllTicketsAsync()
        {
            var ticket = await supportTicketRepository.GetAll().ToListAsync();
            if (ticket is null)
                return ServiceResult<List<SupportTicketResponse>>.Fail("Ticket not found!", HttpStatusCode.NotFound);

            var ticketResponse = ticket.Select(ticket => new SupportTicketResponse(
                   ticket.TicketId,
                   ticket.UserId.ToString(),
                   ticket.Title,
                   ticket.Description,
                   ticket.Status,
                   ticket.Priority,
                   ticket.CreatedDate,
                   ticket.ClosedDate
                )).ToList();
            return ServiceResult<List<SupportTicketResponse>>.Success(ticketResponse);
        }

        public async Task<ServiceResult<SupportTicketResponse>> GetByIdAsync(int ticketId)
        {
            var ticket = await supportTicketRepository.GetByIdDetailAsync(ticketId);
            if (ticket is null)
                return ServiceResult<SupportTicketResponse>.Fail("Ticket not found!", HttpStatusCode.NotFound);

            var ticketResponse = new SupportTicketResponse
            (
                   ticket.TicketId,
                   ticket.User != null ? $"{ticket.User.UserName} {ticket.User.UserLastName}" : "Unknown User",
                   ticket.Title,
                   ticket.Description,
                   ticket.Status,
                   ticket.Priority,
                   ticket.CreatedDate,
                   ticket.ClosedDate
                );
            return ServiceResult<SupportTicketResponse>.Success(ticketResponse);
        }

        public async Task<ServiceResult<CreateSupportTicketResponse>> CreateTicketAsync(CreateSupportTicketRequest request)
        {
            var user = await supportTicketRepository.GetByIdAsync(request.UserId);
            if (user is not null)
                return ServiceResult<CreateSupportTicketResponse>.Fail("Ticket already exists!", HttpStatusCode.BadRequest);

            var newTicket = new SupportTicketModel
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                CreatedDate = DateTime.Now,
            };

            await supportTicketRepository.AddAsync(newTicket);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateSupportTicketResponse>.Success(new CreateSupportTicketResponse(newTicket.TicketId), HttpStatusCode.Created);
        }

        public async Task<ServiceResult> UpdateTicketAsync(int ticketId, UpdateSupportTicketRequest request)
        {
            var ticket = await supportTicketRepository.GetByIdAsync(ticketId);
            if (ticket is null)
                return ServiceResult.Fail("Ticket not found", HttpStatusCode.NotFound);

            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.Status = request.Status;
            ticket.Priority = request.Priority;
            ticket.UpdateDate = DateTime.Now;
            ticket.ClosedDate = request.ClosedDate;

            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);

        }

        public async Task<ServiceResult> DeleteTicketAsync(int ticketId)
        {
            var ticket = await supportTicketRepository.GetByIdAsync(ticketId);
            if (ticket is null)
                return ServiceResult.Fail("Ticket not found", HttpStatusCode.NotFound);

            supportTicketRepository.Delete(ticket);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


        // admin
        public async Task<ServiceResult<List<SupportTicketResponse>>> GetOpenTicketsAsync()
        {
            var openTickets = await supportTicketRepository.GetOpenTicketsAsync();
            if (openTickets is null)
                return ServiceResult<List<SupportTicketResponse>>.Fail("Ticket not found!", HttpStatusCode.NotFound);

            var ticketResponse = openTickets.Select(ticket => new SupportTicketResponse(
                   ticket.TicketId,
                   ticket.User != null ? $"{ticket.User.UserName} {ticket.User.UserLastName}" : "Unknown User",
                   ticket.Title,
                   ticket.Description,
                   ticket.Status,
                   ticket.Priority,
                   ticket.CreatedDate,
                   ticket.ClosedDate
                )).ToList();
            return ServiceResult<List<SupportTicketResponse>>.Success(ticketResponse);
        }

        // Admin
        public async Task<ServiceResult<List<SupportTicketResponse>>> GetTicketsByPriorityAsync(SupportTicketPriority priority)
        {
            var tickets = await supportTicketRepository.GetTicketsByPriorityAsync(priority);

            if (tickets is null)
                return ServiceResult<List<SupportTicketResponse>>.Fail("Ticket not found!", HttpStatusCode.NotFound);

            var ticketResponse = tickets.Select(ticket => new SupportTicketResponse(
                   ticket.TicketId,
                   ticket.User != null ? $"{ticket.User.UserName} {ticket.User.UserLastName}" : "Unknown User",
                   ticket.Title,
                   ticket.Description,
                   ticket.Status,
                   ticket.Priority,
                   ticket.CreatedDate,
                   ticket.ClosedDate
                )).ToList();

            return ServiceResult<List<SupportTicketResponse>>.Success(ticketResponse);
        }

        public async Task<ServiceResult<List<SupportTicketResponse>>> GetTicketsByStatusAsync(SupportTicketStatus status)
        {
            var ticketsStatus = await supportTicketRepository.GetTicketsByStatusAsync(status);

            if (ticketsStatus == null || !ticketsStatus.Any())
                return ServiceResult<List<SupportTicketResponse>>.Fail("Tickets not found!", HttpStatusCode.NotFound);

            var ticketResponse = ticketsStatus.Select(ticket => new SupportTicketResponse(
                ticket.TicketId,
                ticket.User != null ? $"{ticket.User.UserName} {ticket.User.UserLastName}" : "Unknown User",
                ticket.Title,
                ticket.Description,
                ticket.Status,
                ticket.Priority,
                ticket.CreatedDate,
                ticket.ClosedDate
            )).ToList();

            return ServiceResult<List<SupportTicketResponse>>.Success(ticketResponse);
        }

        public async Task<ServiceResult<List<SupportTicketResponse>>> GetTicketsByUserIdAsync(int userId)
        {
            var user = await supportTicketRepository.GetByIdDetailAsync(userId);
            if (user is null)
                return ServiceResult<List<SupportTicketResponse>>.Fail("User not found!", HttpStatusCode.NotFound);

            var userTickets = await supportTicketRepository.GetTicketsByUserIdAsync(userId);
            if (userTickets is null)
                return ServiceResult<List<SupportTicketResponse>>.Fail("User tickets not found!", HttpStatusCode.NotFound);
            var ticketResponse = userTickets.Select(ticket => new SupportTicketResponse(
                   ticket.TicketId,
                   ticket.User != null ? $"{ticket.User.UserName} {ticket.User.UserLastName}" : "Unknown User",
                   ticket.Title,
                   ticket.Description,
                   ticket.Status,
                   ticket.Priority,
                   ticket.CreatedDate,
                   ticket.ClosedDate
                )).ToList();
            return ServiceResult<List<SupportTicketResponse>>.Success(ticketResponse);
        }

        public async Task<ServiceResult<List<SupportTicketResponse>>> GetOpenTicketsByUserIdAsync(int userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if(user is null)
                return ServiceResult<List<SupportTicketResponse>>.Fail("User not found!", HttpStatusCode.NotFound);

            var openTickets = await supportTicketRepository.GetOpenTicketsByUserIdAsync(userId);
            if (openTickets is null)
                return ServiceResult<List<SupportTicketResponse>>.Fail("Open tickets not found!", HttpStatusCode.NotFound);

            var ticketResponse = openTickets.Select(ticket => new SupportTicketResponse(
                   ticket.TicketId,
                   ticket.User != null ? $"{ticket.User.UserName} {ticket.User.UserLastName}" : "Unknown User",
                   ticket.Title,
                   ticket.Description,
                   ticket.Status,
                   ticket.Priority,
                   ticket.CreatedDate,
                   ticket.ClosedDate
                )).ToList();
            return ServiceResult<List<SupportTicketResponse>>.Success(ticketResponse);
        }

        public async Task<ServiceResult> UpdateTicketStatusAsync(UpdateTicketStatusRequest request)
        {
            var ticket = await supportTicketRepository.GetByIdAsync(request.TicketId);
            if (ticket is null)
                return ServiceResult.Fail("Ticket not found", HttpStatusCode.NotFound);

            ticket.Status = request.Status;
            ticket.UpdateDate = DateTime.Now;

            supportTicketRepository.Update(ticket);
            await unitOFWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
