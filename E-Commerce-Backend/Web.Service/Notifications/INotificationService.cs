using Web.Service.Notifications.Create;
using Web.Service.Notifications.Update;

namespace Web.Service.Notifications
{
    public interface INotificationService
    {
        Task<ServiceResult<IEnumerable<NotificationResponse>>> GetAllAsync();
        Task<ServiceResult<NotificationResponse>> GetByIdDetailAsync(int Id);
        Task<ServiceResult<CreateNotificationResponse>> CreateAsync(CreateNotificationRequest request);
        Task<ServiceResult> UpdateAsync(UpdateNotificationRequest request);
        Task<ServiceResult> DeleteAsync(int Id);
    }
}
