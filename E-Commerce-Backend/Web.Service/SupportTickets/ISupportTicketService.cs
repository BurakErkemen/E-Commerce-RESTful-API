using Web.Repository.TicketModels.SupportTickets;
using Web.Service.SupportTickets.Create;
using Web.Service.SupportTickets.Update;

namespace Web.Service.SupportTickets
{
    public interface ISupportTicketService
    {
        Task<ServiceResult<List<SupportTicketResponse>>> GetAllTicketsAsync();
        Task<ServiceResult<SupportTicketResponse>> GetByIdAsync(int ticketId);
        Task<ServiceResult<CreateSupportTicketResponse>> CreateTicketAsync(CreateSupportTicketRequest model);
        Task<ServiceResult> UpdateTicketAsync(int ticketId, UpdateSupportTicketRequest updatedModel);
        Task<ServiceResult> DeleteTicketAsync(int ticketId);

        Task<ServiceResult<List<SupportTicketResponse>>> GetOpenTicketsAsync();
        Task<ServiceResult<List<SupportTicketResponse>>> GetTicketsByUserIdAsync(int userId);
        Task<ServiceResult<List<SupportTicketResponse>>> GetOpenTicketsByUserIdAsync(int userId);
        Task<ServiceResult<List<SupportTicketResponse>>> GetTicketsByStatusAsync(SupportTicketStatus status);
        Task<ServiceResult<List<SupportTicketResponse>>> GetTicketsByPriorityAsync(SupportTicketPriority priority);

        Task<ServiceResult> UpdateTicketStatusAsync(UpdateTicketStatusRequest request);
    }
}
