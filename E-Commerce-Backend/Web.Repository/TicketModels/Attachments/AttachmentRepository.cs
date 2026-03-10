using Microsoft.EntityFrameworkCore;

namespace Web.Repository.TicketModels.Attachments
{
    public class AttachmentRepository(WebDbContext context) : GenericRepository<AttachmentModel>(context), IAttachmentRepository
    {
        public async Task<IEnumerable<AttachmentModel>> GetAllDetailAsync()
        {
            return await Context.Attachments
                .Include(a => a.Ticket)
                .ThenInclude(ticket => ticket.User)
                .ToListAsync();
        }
        public async Task<AttachmentModel?> GetByIdDetailAsync(int attachmentId)
        {
            return await Context.Attachments
                .Include(a => a.Ticket)
                .ThenInclude(ticket => ticket.User)
                .FirstOrDefaultAsync(a => a.Id == attachmentId);
        }
        // Belirli bir TicketId'ye Ait Ekleri Getir
        public async Task<IEnumerable<AttachmentModel>> GetAttachmentsByTicketIdAsync(int ticketId)
        {
            return await Context.Attachments
                .Include(a => a.Ticket)
                .ThenInclude(ticket => ticket.User)
                .Where(a => a.TicketId == ticketId)
                .ToListAsync();
        }
        // Belirli Bir Dosya Boyutundan Büyük Ekleri Getir
        public async Task<IEnumerable<AttachmentModel>> GetLargeAttachmentsAsync(long fileSizeThreshold)
        {
            return await Context.Attachments
                .Include(a => a.Ticket)
                .ThenInclude(ticket => ticket.User)
                .Where(a => a.FileSize > fileSizeThreshold).ToListAsync();
        }

        // Dosya ismi ile arama 
        public async Task<IEnumerable<AttachmentModel>> SearchAttachmentsByFileNameAsync(string fileName)
        {
            return await Context.Attachments
                .Include(a => a.Ticket)
                .ThenInclude(ticket => ticket.User)
                .Where(a => a.FileName.Contains(fileName))
                .ToListAsync();
        }
        // En Büyük Dosya Boyutuna Sahip Ekleri Count Kadar Getirir
        public async Task<IEnumerable<AttachmentModel>> GetTopNLargestAttachmentsAsync(int count)
        {
            return await Context.Attachments
                .Include(a => a.Ticket)
                .ThenInclude(ticket => ticket.User)
                .OrderByDescending(a => a.FileSize)
                .Take(count)
                .ToListAsync();
        }

        // Belirli Bir TicketId'ye Ait Ek Sayısını Getir
        public async Task<int> GetAttachmentCountByTicketIdAsync(int ticketId)
        {
            return await Context.Attachments
                .Include(a => a.Ticket)
                .ThenInclude(ticket => ticket.User)
                .CountAsync(a => a.TicketId == ticketId);
        }

        

        // Belirli Bir Tarihten Sonra Eklenenleri Getir
        // Duruma göre aktif edilebilir fakat model içerisine createdDate de kullanılmalı. 
        /*
        public async Task<IEnumerable<AttachmentModel>> GetRecentAttachmentsAsync(DateTime startDate)
        {
            return await Context.Attachments
                                 .Where(a => a.CreatedDate >= startDate)
                                 .ToListAsync();
        }
        */
    }
}