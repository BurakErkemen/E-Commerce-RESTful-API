using Microsoft.EntityFrameworkCore;

namespace Web.Repository.OrderSection.Order
{
    public class OrderRepository(WebDbContext context) : GenericRepository<OrderModel>(context), IOrderRepository
    {
        public async Task<OrderModel?> GetByIdDetailAsync(int orderId)
        {
            return await Context.Orders
                .Where(o => o.OrderId == orderId)
                .Include(o => o.BasketItems) // Sepetteki ürünleri dahil et
                .ThenInclude(b => b.Product) // Ürün detaylarını dahil et
                .Include(o => o.User)        // Kullanıcı bilgilerini dahil et
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(int userId)
        {
            return await Context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.User) // Kullanıcı bilgilerini de dahil etmek için
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByStatusAsync(string status)
        {
            return await Context.Orders
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByDateAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be greater than end date.");

            return await Context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByTotalAmountAsync(decimal totalAmount)
        {
            return await Context.Orders
                .Where(o => o.TotalAmount >= totalAmount)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByStatusAndDateAsync(string status, DateTime startDate, DateTime endDate)
        {
            // Durum ve tarih aralığına göre siparişleri getirir
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be greater than end date.");

            return await Context.Orders
                .Where(o => o.Status == status && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }

        public async Task<OrderModel?> GetOrderWithPaymentsAsync(int orderId)
        {
            return await Context.Orders
                .Include(order => order.Payments)
                .FirstOrDefaultAsync(order => order.OrderId == orderId);
        }
    }
}
