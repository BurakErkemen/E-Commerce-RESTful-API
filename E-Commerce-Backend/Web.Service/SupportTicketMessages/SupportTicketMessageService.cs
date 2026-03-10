using Web.Repository;
using System.Net;
using Web.Repository.TicketModels.SupportTicketMessages;
using Web.Service.SupportTicketMessages.Create;
using Web.Service.SupportTicketMessages.Update;
using Microsoft.EntityFrameworkCore;
using Web.Repository.UserInfo.Users;
using Azure.Core;

namespace Web.Service.SupportTicketMessages
{
    public class SupportTicketMessageService
        (ISupportTicketMessageRepository supportTicketMessageRepository,
        IUserRepository userRepository,
        IUnitOFWork unitOfWork) : ISupportTicketMessageService
    {
        public async Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetAll()
        {
            var tickets = await supportTicketMessageRepository.GetAllDetailAsync();
            if (tickets is null)
                return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Fail("No tickets found", HttpStatusCode.NotFound);

            var response = tickets.Select(t => new SupportTicketMessageResponse(
                t.TicketId,
                t.Ticket.Title,
                t.SenderId,
                t.Sender != null ? $"{t.Sender.UserName} {t.Sender.UserLastName}" : "Unknown User",
                t.MessageContent,
                t.SentDate
            ));

            return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Success(response);
        }

        public async Task<ServiceResult<SupportTicketMessageResponse>> GetById(int messageId)
        {
            var message = await supportTicketMessageRepository.GetByIdDetailAsync(messageId);
            if (message is null)
                return ServiceResult<SupportTicketMessageResponse>.Fail("Message not found", HttpStatusCode.NotFound);

            var response = new SupportTicketMessageResponse(
                message.TicketId,
                message.Ticket.Title,
                message.SenderId,
                message.Sender != null ? $"{message.Sender.UserName} {message.Sender.UserLastName}" : "Unknown User",
                message.MessageContent,
                message.SentDate
            );

            return ServiceResult<SupportTicketMessageResponse>.Success(response);
        }

        public async Task<ServiceResult<CreateSupportTicketMessageResponse>> Create(CreateSupportTicketMessageRequest request)
        {
            var user = await userRepository.GetByIdAsync(request.SenderId);
            if (user is null)
                return ServiceResult<CreateSupportTicketMessageResponse>.Fail("User not found", HttpStatusCode.NotFound);

            var message = new SupportTicketMessageModel
            {
                TicketId = request.TicketId,
                SenderId = request.SenderId,
                MessageContent = request.MessageContent
            };

            await supportTicketMessageRepository.AddAsync(message);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateSupportTicketMessageResponse>.Success(new CreateSupportTicketMessageResponse(message.MessageId));
        }

        public async Task<ServiceResult> Update(UpdateSupportTicketMessageRequest request)
        {
            var message = await supportTicketMessageRepository.GetByIdAsync(request.MessageId);
            if (message is null)
                return ServiceResult.Fail("Message not found", HttpStatusCode.NotFound);

            message.MessageContent = request.MessageContent;
            message.SentDate = DateTime.Now;

            supportTicketMessageRepository.Update(message);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> Delete(int messageId)
        {
            var message = await supportTicketMessageRepository.GetByIdAsync(messageId);
            if (message is null)
                return ServiceResult.Fail("Message not found", HttpStatusCode.NotFound);


            supportTicketMessageRepository.Delete(message);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


        public async Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesByTicketIdAsync(int ticketId)
        {
            var messages = await supportTicketMessageRepository.GetMessagesByTicketIdAsync(ticketId);
            if (messages is null)
                return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Fail("No messages found", HttpStatusCode.NotFound);

            var response = messages.Select(m => new SupportTicketMessageResponse(
                m.TicketId,
                m.Ticket.Title,
                m.SenderId,
                m.Sender != null ? $"{m.Sender.UserName} {m.Sender.UserLastName}" : "Unknown User",
                m.MessageContent,
                m.SentDate
            ));
            return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Success(response);
        }

        public async Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesBySenderIdAsync(int senderId)
        {
            var messages = await supportTicketMessageRepository.GetMessagesBySenderIdAsync(senderId);
            if (messages is null)
                return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Fail("No messages found", HttpStatusCode.NotFound);

            var response = messages.Select(m => new SupportTicketMessageResponse(
                m.TicketId,
                m.Ticket.Title,
                m.SenderId,
                m.Sender != null ? $"{m.Sender.UserName} {m.Sender.UserLastName}" : "Unknown User",
                m.MessageContent,
                m.SentDate
            ));

            return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Success(response);
        }

        public async  Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesAfterDateAsync(DateTime startDate)
        {
            var messages = await supportTicketMessageRepository.GetMessagesAfterDateAsync(startDate);
            if (messages is null)
                return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Fail("No messages found", HttpStatusCode.NotFound);

            var response = messages.Select(m => new SupportTicketMessageResponse(
                m.TicketId,
                m.Ticket.Title,
                m.SenderId,
                m.Sender != null ? $"{m.Sender.UserName} {m.Sender.UserLastName}" : "Unknown User",
                m.MessageContent,
                m.SentDate
            ));

            return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Success(response);
        }

        public async Task<ServiceResult<IEnumerable<SupportTicketMessageResponse>>> GetMessagesWithinDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var beetweenDateMessage = await supportTicketMessageRepository.GetMessagesWithinDateRangeAsync(startDate, endDate);
            if (beetweenDateMessage is null)
                return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Fail("No messages found", HttpStatusCode.NotFound);

            var response = beetweenDateMessage.Select(m => new SupportTicketMessageResponse(
                m.TicketId,
                m.Ticket.Title,
                m.SenderId,
                m.Sender != null ? $"{m.Sender.UserName} {m.Sender.UserLastName}" : "Unknown User",
                m.MessageContent,
                m.SentDate
            ));

            return ServiceResult<IEnumerable<SupportTicketMessageResponse>>.Success(response);
        }

        public async Task<ServiceResult<int>> GetMessageCountByTicketIdAsync(int ticketId)
        {
            var messageCount = await supportTicketMessageRepository.GetMessageCountByTicketIdAsync(ticketId);
            return ServiceResult<int>.Success(messageCount);
        }

        public async Task<ServiceResult<int>> GetMessageCountBySenderIdAsync(int senderId)
        {
            var messageCount = await supportTicketMessageRepository.GetMessageCountBySenderIdAsync(senderId);
            return ServiceResult<int>.Success(messageCount);
        }
    }
}
