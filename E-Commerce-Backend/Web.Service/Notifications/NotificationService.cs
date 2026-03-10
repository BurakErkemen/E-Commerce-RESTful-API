using System.Net;
using Web.Repository;
using Web.Repository.Admin.Notifications;
using Web.Service.Notifications.Create;
using Web.Service.Notifications.Update;

namespace Web.Service.Notifications
{
    public class NotificationService
        (
        INotificationRepository notificationRepository,
        IUnitOFWork unitOFWork
        ) : INotificationService
    {
        public async Task<ServiceResult<IEnumerable<NotificationResponse>>> GetAllAsync()
        {
            var notification = await notificationRepository.GetAllDetailAsync(); 
            if (notification is null || !notification.Any())
                return ServiceResult<IEnumerable<NotificationResponse>>.Fail("Notification not found", HttpStatusCode.NotFound);

            var notificationAsResponse = notification.Select(notification => new NotificationResponse(
                UserEmail: notification!.UserEmail,
                Message: notification.Message,
                MessageDate: notification.MessageDate
            ));

            return ServiceResult<IEnumerable<NotificationResponse>>.Success(notificationAsResponse);
        }

        public async Task<ServiceResult<NotificationResponse>> GetByIdDetailAsync(int Id)
        {
            var notification = await notificationRepository.GetByIdDetailAsync(Id);
            if(notification is null)
                return ServiceResult<NotificationResponse>.Fail("Notification not found", HttpStatusCode.NotFound);

            var notificationAsResponse = new NotificationResponse(
                UserEmail: notification.UserEmail,
                Message: notification.Message,
                MessageDate: notification.MessageDate
            );

            return ServiceResult<NotificationResponse>.Success(notificationAsResponse);
        }

        public async Task<ServiceResult<CreateNotificationResponse>> CreateAsync(CreateNotificationRequest request)
        {
            var notification = new NotificationModel
            {
                UserEmail = request.UserEmail,
                Message = request.Message,
                MessageDate = DateTime.Now
            };
            await notificationRepository.AddAsync(notification);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateNotificationResponse>.Success(new CreateNotificationResponse(notification.NotificationId));
        }

        public async Task<ServiceResult> UpdateAsync(UpdateNotificationRequest request)
        {
            var notification = await notificationRepository.GetByIdDetailAsync(request.Id);
            if (notification is null)
                return ServiceResult.Fail("Notification not found", HttpStatusCode.NotFound);

            notification.UserEmail = request.UserEmail;
            notification.Message = request.Message;
            notification.MessageDate = DateTime.Now;

            notificationRepository.Update(notification);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int Id)
        {
            var notification = await notificationRepository.GetByIdAsync(Id);
            if(notification is null) 
                return ServiceResult.Fail("Notification not found",HttpStatusCode.NotFound);

            notificationRepository.Delete(notification);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.OK);
        }
    }
}
