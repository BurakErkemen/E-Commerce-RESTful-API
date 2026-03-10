using Web.Service.Attachments.Create;
using Web.Service.Attachments.Update;

namespace Web.Service.Attachments
{
    public interface IAttachmentService
    {
        Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetAll();
        Task<ServiceResult<AttachmentResponse>> GetById(int Id);
        Task<ServiceResult<CreateAttachmentResponse>> Create(CreateAttachmentRequest request);
        Task<ServiceResult> Update(UpdateAttachmentRequest request);
        Task<ServiceResult> Delete(int Id);

        // Custom methods
        Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetAttachmentsByTicketIdAsync(int ticketId); // Belirli bir TicketId'ye Ait Ekleri Getir
        Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetLargeAttachmentsAsync(long fileSizeThreshold); // Belirli Bir Dosya Boyutundan Büyük Ekleri Getir
        Task<ServiceResult<IEnumerable<AttachmentResponse>>> SearchAttachmentsByFileNameAsync(string fileName); // Dosya ismi ile arama
        Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetTopNLargestAttachmentsAsync(int count); // En Büyük N Dosya Boyutuna Sahip Ekleri Getir
        Task<ServiceResult<int>> GetAttachmentCountByTicketIdAsync(int ticketId); // Belirli Bir TicketId'ye Ait Ek Sayısını Getir

    }
}
