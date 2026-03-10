namespace Web.Repository.TicketModels.SupportTicketMessages
{
    public interface ISupportTicketMessageRepository : IGenericRepository<SupportTicketMessageModel>
    {
        Task<List<SupportTicketMessageModel>> GetAllDetailAsync();
        Task<SupportTicketMessageModel?> GetByIdDetailAsync(int Id);
        Task<IEnumerable<SupportTicketMessageModel>> GetMessagesByTicketIdAsync(int ticketId);
        Task<IEnumerable<SupportTicketMessageModel>> GetMessagesBySenderIdAsync(int senderId);
        Task<IEnumerable<SupportTicketMessageModel>> GetMessagesAfterDateAsync(DateTime startDate);
        Task<IEnumerable<SupportTicketMessageModel>> GetMessagesWithinDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<int> GetMessageCountByTicketIdAsync(int ticketId);
        Task<int> GetMessageCountBySenderIdAsync(int senderId);
    }
}
