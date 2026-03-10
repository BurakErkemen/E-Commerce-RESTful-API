
using Microsoft.EntityFrameworkCore;

namespace Web.Repository.TicketModels.SupportTickets
{
    public class SupportTicketRepository(WebDbContext context) : GenericRepository<SupportTicketModel>(context), ISupportTicketRepository
    {
        public async Task<SupportTicketModel?> GetByIdDetailAsync(int ticketId)
        {
            return await Context.SupportTickets
                .Include(ticket => ticket.User) // Kullanıcı bilgisi dahil
                .Include(ticket => ticket.Attachments) // Ek dosyalar dahil
                .Include(ticket => ticket.Messages) // Mesaj geçmişi dahil
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        }

        public async Task<List<SupportTicketModel>> GetOpenTicketsAsync()
        {
            return await Context.SupportTickets
                .Include(ticket => ticket.User) // Kullanıcı bilgisi dahil
                .Include(ticket => ticket.Attachments) // Ek dosyalar dahil
                .Include(ticket => ticket.Messages) // Mesaj geçmişi dahil
                .Where(ticket => ticket.Status == SupportTicketStatus.Open)
                .ToListAsync();
        }

        public async Task<List<SupportTicketModel>> GetTicketsByPriorityAsync(SupportTicketPriority priority)
        {
            return await Context.SupportTickets
                .Include(t => t.User)
                .Where(ticket => ticket.Priority == priority)
                .ToListAsync();
        }
        public async Task<List<SupportTicketModel>> GetOpenTicketsByUserIdAsync(int userId)
        {
            return await Context.SupportTickets
                .Include(ticket => ticket.User) // Kullanıcı bilgisi dahil
                .Include(ticket => ticket.Attachments) // Ek dosyalar dahil
                .Include(ticket => ticket.Messages) // Mesaj geçmişi dahil
                .Where(ticket => ticket.Status == SupportTicketStatus.Open && ticket.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<SupportTicketModel>> GetTicketsByStatusAsync(SupportTicketStatus status)
        {
            return await Context.SupportTickets
                .Include(ticket => ticket.User) // Kullanıcı bilgisi dahil
                .Include(ticket => ticket.Attachments) // Ek dosyalar dahil
                .Where(ticket => ticket.Status == status)
                .ToListAsync();
        }

        public async Task<List<SupportTicketModel>> GetTicketsByUserIdAsync(int userId)
        {
            return await Context.SupportTickets
                .Include(ticket => ticket.Attachments) // Ek dosyalar dahil
                .Include(ticket => ticket.Messages) // Mesaj geçmişi dahil
                .Where(ticket => ticket.UserId == userId)
                .ToListAsync();
        }

        /*public async Task<SupportTicketModel?> UpdateTicketStatusAsync(int ticketId, SupportTicketStatus newStatus)
        {
            var ticket = await Context.SupportTickets.FirstOrDefaultAsync(t => t.TicketId == ticketId);
            if (ticket == null) return null;

            ticket.Status = newStatus;
            await Context.SaveChangesAsync();

            return ticket;
        }*/

    }
}
