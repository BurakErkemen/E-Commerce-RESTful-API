using Microsoft.EntityFrameworkCore;

namespace Web.Repository.TicketModels.SupportTicketMessages
{
    public class SupportTicketMessageRepository(WebDbContext context) :
        GenericRepository<SupportTicketMessageModel>(context), ISupportTicketMessageRepository
    {
        public async Task<List<SupportTicketMessageModel>> GetAllDetailAsync()
        {
            return await Context.SupportTicketMessages
                                 .Include(msg => msg.Ticket) // Talep bilgilerini dahil et
                                 .Include(msg => msg.Sender) // Gönderen kullanıcı bilgilerini dahil et
                                 .ToListAsync();
        }
        public async Task<SupportTicketMessageModel?> GetByIdDetailAsync(int Id)
        {
            return await Context.SupportTicketMessages
                                 .Include(msg => msg.Ticket) // Talep bilgilerini dahil et
                                 .Include(msg => msg.Sender) // Gönderen kullanıcı bilgilerini dahil et
                                 .FirstOrDefaultAsync(msg => msg.MessageId== Id);
        } 

        // Belirli Bir TicketId İçin Mesajları Getir
        public async Task<IEnumerable<SupportTicketMessageModel>> GetMessagesByTicketIdAsync(int ticketId)
        {
            return await Context.SupportTicketMessages
                                 .Include(msg => msg.Ticket) // Talep bilgilerini dahil et
                                 .Include(msg => msg.Sender) // Gönderen kullanıcı bilgilerini dahil et
                                 .Where(msg => msg.TicketId == ticketId)
                                 .OrderBy(msg => msg.SentDate) // Tarihe göre sıralama
                                 .ToListAsync();
        }

        // Belirli Bir Kullanıcı Tarafından Gönderilen Mesajlar
        public async Task<IEnumerable<SupportTicketMessageModel>> GetMessagesBySenderIdAsync(int senderId)
        {
            return await Context.SupportTicketMessages
                                 .Include(msg => msg.Ticket) // Talep bilgilerini dahil et
                                 .Include(msg => msg.Sender) // Gönderen kullanıcı bilgilerini dahil et
                                 .Where(msg => msg.SenderId == senderId)
                                 .ToListAsync();
        }

        // Belirli Bir Tarihten Sonra Gönderilen Mesajları Getir
        public async Task<IEnumerable<SupportTicketMessageModel>> GetMessagesAfterDateAsync(DateTime startDate)
        {
            return await Context.SupportTicketMessages
                                 .Include(msg => msg.Ticket) // Talep bilgilerini dahil et
                                 .Include(msg => msg.Sender) // Gönderen kullanıcı bilgilerini dahil et
                                 .Where(msg => msg.SentDate >= startDate)
                                 .ToListAsync();
        }

        // Belirli Bir Süre İçindeki Mesajları Getir
        public async Task<IEnumerable<SupportTicketMessageModel>> GetMessagesWithinDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await Context.SupportTicketMessages
                                 .Include(msg => msg.Ticket) // Talep bilgilerini dahil et
                                 .Include(msg => msg.Sender) // Gönderen kullanıcı bilgilerini dahil et
                                 .Where(msg => msg.SentDate >= startDate && msg.SentDate <= endDate)
                                 .ToListAsync();
        }

        // Belirli Bir TicketId'ye Ait Mesaj Sayısını Getir
        public async Task<int> GetMessageCountByTicketIdAsync(int ticketId)
        {
            return await Context.SupportTicketMessages
                                 .CountAsync(msg => msg.TicketId == ticketId);
        }

        // Belirli Bir Kullanıcı Tarafından Gönderilen Mesaj Sayısını Getir
        public async Task<int> GetMessageCountBySenderIdAsync(int senderId)
        {
            return await Context.SupportTicketMessages
                                 .CountAsync(msg => msg.SenderId == senderId);
        }
    }
}