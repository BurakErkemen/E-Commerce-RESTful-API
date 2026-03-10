using Iyzipay;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Web.Repository.OrderSection.Payment
{
    public class PaymentRepository : GenericRepository<PaymentModel>, IPaymentRepository
    {
        private readonly Iyzipay.Options _options;

        // Constructor'da Options nesnesi enjekte ediliyor
        public PaymentRepository(WebDbContext context, IConfiguration configuration)
            : base(context)
        {
            _options = new Iyzipay.Options
            {
                ApiKey = configuration["Iyzico:ApiKey"],
                SecretKey = configuration["Iyzico:SecretKey"],
                BaseUrl = configuration["Iyzico:BaseUrl"]
            };
        }

        // OrderId'ye göre ödeme getir
        public async Task<PaymentModel?> GetByOrderIdAsync(int orderId)
        {
            return await Context.Set<PaymentModel>()
                                 .FirstOrDefaultAsync(payment => payment.OrderId == orderId);
        }

        // Belirli bir tarih aralığındaki ödemeleri getir
        public async Task<IEnumerable<PaymentModel>> GetPaymentsByDateAsync(DateTime startDate, DateTime endDate)
        {
            return await Context.Set<PaymentModel>()
                                 .Where(payment => payment.PaymentDate >= startDate && payment.PaymentDate <= endDate)
                                 .ToListAsync();
        }

        // Ödeme durumuna göre ödemeleri getir
        public async Task<IEnumerable<PaymentModel>> GetPaymentsByStatusAsync(string status)
        {
            return await Context.Set<PaymentModel>()
                                 .Where(payment => payment.Status == status)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<PaymentModel>> GetPaymentsByOrderIdAsync(int orderId)
        {
            return await Context.Payments
                .Where(payment => payment.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<Iyzipay.Model.Payment> ProcessPaymentAsync(CreatePaymentRequest request)
        {
            return await Task.Run(() => Iyzipay.Model.Payment.Create(request, _options));
        }
    }
}