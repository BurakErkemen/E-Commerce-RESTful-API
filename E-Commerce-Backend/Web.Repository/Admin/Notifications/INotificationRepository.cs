namespace Web.Repository.Admin.Notifications
{
    public interface INotificationRepository : IGenericRepository<NotificationModel>
    {
        public Task<NotificationModel?> GetByIdDetailAsync(int id);
        public Task<IEnumerable<NotificationModel?>> GetAllDetailAsync();
    }
}
