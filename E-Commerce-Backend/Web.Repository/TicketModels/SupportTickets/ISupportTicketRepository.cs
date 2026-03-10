namespace Web.Repository.TicketModels.SupportTickets
{
    public interface ISupportTicketRepository : IGenericRepository<SupportTicketModel>
    {
        Task<List<SupportTicketModel>> GetOpenTicketsAsync(); //Açık olan talepler
        Task<SupportTicketModel?> GetByIdDetailAsync(int Id);
        Task<List<SupportTicketModel>> GetTicketsByUserIdAsync(int userId); //Kullanıcıya ait talepler
        Task<List<SupportTicketModel>> GetTicketsByStatusAsync(SupportTicketStatus status); //Duruma göre talepler
        Task<List<SupportTicketModel>> GetTicketsByPriorityAsync(SupportTicketPriority priority); //Öncelikli talepleri listelemek için
        Task<List<SupportTicketModel>> GetOpenTicketsByUserIdAsync(int userId); //Kullanıcıya ait açık talepler

        // Task<SupportTicketModel?> UpdateTicketStatusAsync(int ticketId, SupportTicketStatus newStatus); //Talep durumunu güncellemek için
    }
}
