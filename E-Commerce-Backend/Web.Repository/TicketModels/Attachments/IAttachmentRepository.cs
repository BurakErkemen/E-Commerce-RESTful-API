using Microsoft.EntityFrameworkCore;
using Web.Repository.TicketModels.Attachments;

namespace Web.Repository.TicketModels.Attachments
{
    public interface IAttachmentRepository : IGenericRepository<AttachmentModel>
    {
        Task<IEnumerable<AttachmentModel>> GetAllDetailAsync();
        Task<IEnumerable<AttachmentModel>> GetAttachmentsByTicketIdAsync(int ticketId);
        Task<IEnumerable<AttachmentModel>> GetLargeAttachmentsAsync(long fileSizeThreshold);
        Task<IEnumerable<AttachmentModel>> SearchAttachmentsByFileNameAsync(string fileName);
        Task<IEnumerable<AttachmentModel>> GetTopNLargestAttachmentsAsync(int count);
        Task<int> GetAttachmentCountByTicketIdAsync(int ticketId);
        Task<AttachmentModel?> GetByIdDetailAsync(int attachmentId);
        //Duruma göre aktif edilebilir fakat model içerisine createdDate de kullanılmalı.
        //Task<IEnumerable<AttachmentModel>> GetRecentAttachmentsAsync(DateTime startDate);
    }
}