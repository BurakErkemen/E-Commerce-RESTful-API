using System.Net;
using Web.Repository;
using Web.Repository.TicketModels.Attachments;
using Web.Repository.TicketModels.SupportTickets;
using Web.Service.Attachments.Create;
using Web.Service.Attachments.Update;

namespace Web.Service.Attachments
{
    public class AttachmentService(
        IAttachmentRepository attachmentRepository,
        ISupportTicketRepository supportTicketRepository,
        IUnitOFWork unitOFWork) : IAttachmentService
    {

        public async Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetAll()
        {
            var attachments = await attachmentRepository.GetAllDetailAsync();
            if (attachments == null || !attachments.Any())
                return ServiceResult<IEnumerable<AttachmentResponse>>.Fail("Attachments not found", HttpStatusCode.NotFound);

            var attachmentsAsResponse = attachments.Select(attachment => new AttachmentResponse(
                FileName: attachment.FileName,
                Url: attachment.Url,
                FileSize: attachment.FileSize,
                TicketId: attachment.TicketId,
                TicketTitle: attachment.Ticket.Title,
                UserName: attachment.Ticket.User.UserName
            ));

            return ServiceResult<IEnumerable<AttachmentResponse>>.Success(attachmentsAsResponse);
        }

        public async Task<ServiceResult<AttachmentResponse>> GetById(int Id)
        {
            var attachment = await attachmentRepository.GetByIdDetailAsync(Id);
            if (attachment == null)
                return ServiceResult<AttachmentResponse>.Fail("Attachment not found", HttpStatusCode.NotFound);

            var attachmentAsResponse = new AttachmentResponse(
                FileName: attachment.FileName,
                Url: attachment.Url,
                FileSize: attachment.FileSize,
                TicketId: attachment.TicketId,
                TicketTitle: attachment.Ticket.Title,
                UserName: attachment.Ticket.User.UserName
            );

            return ServiceResult<AttachmentResponse>.Success(attachmentAsResponse);
        }

        public async Task<ServiceResult<CreateAttachmentResponse>> Create(CreateAttachmentRequest request)
        {
            var ticketExists = await supportTicketRepository.GetByIdDetailAsync(request.TicketId);
            if (ticketExists is null)
            {
                return ServiceResult<CreateAttachmentResponse>.Fail("Ticket does not exist.", HttpStatusCode.NotFound);
            }

            var attachment = new AttachmentModel
            {
                FileName = request.FileName,
                Url = request.Url,
                FileSize = request.FileSize,
                TicketId = request.TicketId
            };

            await attachmentRepository.AddAsync(attachment);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateAttachmentResponse>.Success(new CreateAttachmentResponse(attachment.Id), HttpStatusCode.Created);
        }

        public async Task<ServiceResult> Update(UpdateAttachmentRequest request)
        {
            var ticket = await attachmentRepository.GetByIdAsync(request.Id);
            if (ticket == null)
                return ServiceResult.Fail("Attachment not found", HttpStatusCode.NotFound);

            ticket.FileName = request.FileName;
            ticket.Url = request.Url;
            ticket.FileSize = request.FileSize;

            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
        
        public async Task<ServiceResult> Delete(int Id)
        {
            var attachment = await attachmentRepository.GetByIdAsync(Id);
            if (attachment == null)
                return ServiceResult.Fail("Attachment not found", HttpStatusCode.NotFound);

            attachmentRepository.Delete(attachment);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }




        public async Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetLargeAttachmentsAsync(long fileSizeThreshold)
        {
            var fileSize = await attachmentRepository.GetLargeAttachmentsAsync(fileSizeThreshold);
            if (fileSize == null || !fileSize.Any())
                return ServiceResult<IEnumerable<AttachmentResponse>>.Fail("Attachments not found", System.Net.HttpStatusCode.NotFound);

            var fileSizeAsResponse = fileSize.Select(attachment => new AttachmentResponse(
                FileName: attachment.FileName,
                Url: attachment.Url,
                FileSize: attachment.FileSize,
                TicketId: attachment.TicketId,
                TicketTitle: attachment.Ticket.Title,
                UserName: attachment.Ticket.User.UserName
            ));

            return ServiceResult<IEnumerable<AttachmentResponse>>.Success(fileSizeAsResponse);
        }

        public async Task<ServiceResult<IEnumerable<AttachmentResponse>>> SearchAttachmentsByFileNameAsync(string fileName)
        {
            var fileNames = await attachmentRepository.SearchAttachmentsByFileNameAsync(fileName);
            if (fileNames == null || !fileNames.Any())
                return ServiceResult<IEnumerable<AttachmentResponse>>.Fail("Attachments not found", System.Net.HttpStatusCode.NotFound);

            var fileNamesAsResponse = fileNames.Select(attachment => new AttachmentResponse(
                FileName: attachment.FileName,
                Url: attachment.Url,
                FileSize: attachment.FileSize,
                TicketId: attachment.TicketId,
                TicketTitle: attachment.Ticket.Title,
                UserName: attachment.Ticket.User.UserName
            ));
            return ServiceResult<IEnumerable<AttachmentResponse>>.Success(fileNamesAsResponse);
        }

        public async Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetTopNLargestAttachmentsAsync(int count)
        {
            var attachments = await attachmentRepository.GetTopNLargestAttachmentsAsync(count);
            if (attachments == null || !attachments.Any())
                return ServiceResult<IEnumerable<AttachmentResponse>>.Fail("Attachments not found", System.Net.HttpStatusCode.NotFound);

            var attachmentsAsResponse = attachments.Select(attachment => new AttachmentResponse(
                FileName: attachment.FileName,
                Url: attachment.Url,
                FileSize: attachment.FileSize,
                TicketId: attachment.TicketId,
                TicketTitle: attachment.Ticket.Title,
                UserName: attachment.Ticket.User.UserName
            ));

            return ServiceResult<IEnumerable<AttachmentResponse>>.Success(attachmentsAsResponse);
        }

        public async Task<ServiceResult<IEnumerable<AttachmentResponse>>> GetAttachmentsByTicketIdAsync(int ticketId)
        {
            var attachments = await attachmentRepository.GetAttachmentsByTicketIdAsync(ticketId);
            if (attachments == null || !attachments.Any())
                return ServiceResult<IEnumerable<AttachmentResponse>>.Fail("Attachments not found", System.Net.HttpStatusCode.NotFound);

            var attachmentsAsResponse = attachments.Select(attachment => new AttachmentResponse(
                FileName: attachment.FileName,
                Url: attachment.Url,
                FileSize: attachment.FileSize,
                TicketId: attachment.TicketId,
                TicketTitle: attachment.Ticket.Title,
                UserName: attachment.Ticket.User.UserName
            ));

            return ServiceResult<IEnumerable<AttachmentResponse>>.Success(attachmentsAsResponse);
        }


        public async Task<ServiceResult<int>> GetAttachmentCountByTicketIdAsync(int ticketId)
        {
            var attachmentCount = await attachmentRepository.GetAttachmentCountByTicketIdAsync(ticketId);
            return ServiceResult<int>.Success(attachmentCount,HttpStatusCode.OK);
        }
    }
}
