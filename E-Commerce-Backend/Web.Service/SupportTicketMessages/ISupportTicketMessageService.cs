using Web.Service.SupportTicketMessages.Create;
using Web.Service.SupportTicketMessages.Update;

namespace Web.Service.SupportTicketMessages
{
    public interface ISupportTicketMessageService
    {
        Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetAll();
        Task<ServiceResult<SupportTicketMessageResponse>> GetById(int messageId);
        Task<ServiceResult<CreateSupportTicketMessageResponse>> Create(CreateSupportTicketMessageRequest request);
        Task<ServiceResult> Update(UpdateSupportTicketMessageRequest request);
        Task<ServiceResult> Delete(int messageId);


        // Belirli Bir TicketId İçin Mesajları Getir
        Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesByTicketIdAsync(int ticketId);
        // Belirli Bir Kullanıcı Tarafından Gönderilen Mesajlar
        Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesBySenderIdAsync(int senderId);
        // Belirli Bir Tarihten Sonra Gönderilen Mesajları Getir
        Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesAfterDateAsync(DateTime startDate);
        // Belirli Bir Süre İçindeki Mesajları Getir
        Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesWithinDateRangeAsync(DateTime startDate, DateTime endDate);
        // Belirli Bir TicketId'ye Ait Mesaj Sayısını Getir
        Task<ServiceResult<int>> GetMessageCountByTicketIdAsync(int ticketId);
        // Belirli bir kullanıcı tarafından gönderilen mesaj sayısını getir
        Task<ServiceResult<int>> GetMessageCountBySenderIdAsync(int senderId);
    }
}
