    
using Microsoft.EntityFrameworkCore;

namespace Web.Repository.Admin.Notifications
{
    public class NotificationRepository(WebDbContext context) : GenericRepository<NotificationModel>(context), INotificationRepository
    {
        public async Task<IEnumerable<NotificationModel?>> GetAllDetailAsync()
        {
            return await Context.NotificationModels
                .Include(x => x.Users)
                .ToListAsync();
        }

        public async Task<NotificationModel?> GetByIdDetailAsync(int id)
        {
            return await Context.NotificationModels
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.NotificationId == id);
        }
    }
}
