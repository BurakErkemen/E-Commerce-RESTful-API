using Iyzipay.Model;
using Iyzipay.Request;
using System.Net;
using Web.Repository;
using Web.Repository.OrderSection.BasketItem;
using Web.Repository.OrderSection.Order;
using Web.Repository.OrderSection.Payment;
using Web.Repository.UserInfo.Users;
using Web.Service.Payments.Create;

namespace Web.Service.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IBasketItemRepository basketItemRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOFWork unitOFWork;
        private readonly Iyzipay.Options _options;
        public PaymentService(IPaymentRepository paymentRepository,
            IUserRepository userRepository,
            IOrderRepository orderRepository,
            IBasketItemRepository basketItemRepository,
            IUnitOFWork unitOFWork)
        {
            this.paymentRepository = paymentRepository;
            this.orderRepository = orderRepository;
            this.basketItemRepository = basketItemRepository;
            this.userRepository = userRepository;
            this.unitOFWork = unitOFWork;
            _options = new Iyzipay.Options
            {
                ApiKey = "sandbox-QrGAh6curdDf6UCmhb3NqP5DcufdSAtT",
                SecretKey = "sandbox-XGaz1RTemA89p9VCxoFfPvYCneKkWrr7",
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };
        }

        public async Task<ServiceResult<PaymentResponse>> ProcessPaymentAsync(List<int> basketItemIds, int userId, CreatePaymentRequest request)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user is null)
                return ServiceResult<PaymentResponse>.Fail("User not found.", HttpStatusCode.NotFound);



            request.Locale = Locale.TR.ToString();
            request.ConversationId = user.UserId.ToString();
            request.Price = request.Price.ToString();
            request.PaidPrice = request.PaidPrice.ToString();
            request.Installment = 1;
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new()
            {
                CardHolderName = request.PaymentCard.CardHolderName,
                CardNumber = request.PaymentCard.CardNumber,
                ExpireMonth = request.PaymentCard.ExpireMonth,
                ExpireYear = request.PaymentCard.ExpireYear,
                Cvc = request.PaymentCard.Cvc,
                RegisterCard = 0
            };

            Buyer buyer = new()
            {
                Id = user.UserId.ToString(),
                Name = user.UserName,
                Surname = user.UserLastName,
                GsmNumber = user.UserPhoneNumber,
                Email = user.UserEmail,
                IdentityNumber = null,
                LastLoginDate = null,
                RegistrationDate = user.CreateDate.ToString(),
                RegistrationAddress = user.Addresses?
                .Select(a => $"{a.AddressLine}, {a.City}, {a.Country}, {a.PostCode}")
                .FirstOrDefault() ?? "No address provided",
                Ip = request.Buyer.Ip,
                City = request.Buyer.City,
                Country = request.Buyer.Country,
                ZipCode = request.Buyer.ZipCode
            };

            Address shipingAddress = new()
            {
                ContactName = request.ShippingAddress.ContactName,
                City = request.ShippingAddress.City,
                Country = request.ShippingAddress.Country,
                Description = request.ShippingAddress.Description,
                ZipCode = request.ShippingAddress.ZipCode
            };

            Address billingAddress = new()
            {
                ContactName = request.BillingAddress.ContactName,
                City = request.BillingAddress.City,
                Country = request.BillingAddress.Country,
                Description = request.BillingAddress.Description,
                ZipCode = request.BillingAddress.ZipCode
            };

            List<BasketItemModel> basketItems = [];

            foreach (var basketItemId in basketItemIds) // Kullanıcının sepetindeki öğelerin ID'lerini içeren liste
            {
                // Sepet öğesini veritabanından getir
                var basketItem = await basketItemRepository.GetByIdAsync(basketItemId);

                // Sepet öğesi bulunamazsa devam et
                if (basketItem == null)
                    continue;

                // Yeni bir BasketItemModel oluştur ve verileri ekle
                BasketItemModel basketItemModel = new()
                {
                    BasketItemId = basketItem.BasketItemId,
                    UserId = basketItem.UserId,
                    ProductId = basketItem.ProductId,
                    Quantity = basketItem.Quantity,
                    UnitPrice = basketItem.UnitPrice,
                    Price = basketItem.UnitPrice * basketItem.Quantity, // Toplam fiyat hesaplanıyor

                    // Navigasyon özelliklerini eşitle
                    User = basketItem.User,
                    Product = basketItem.Product
                };

                // Listeye ekle
                basketItems.Add(basketItemModel);
            }

            // Bu noktada basketItems, tüm geçerli sepet öğelerini içerir.

            Payment payment = await Payment.Create(request, _options);

            // ordermodel oluştur
            OrderModel orderModel = new()
            {
                UserId = user.UserId,
                TotalAmount = Convert.ToDecimal(payment.PaidPrice),
                OrderDate = DateTime.Now,
                Status = payment.Status
            };
            await orderRepository.AddAsync(orderModel);
            await unitOFWork.SaveChangesAsync();

            if (payment == null || payment.PaidPrice == null || payment.Status == null || payment.CardType == null || payment.PaymentId == null)
            {
                return ServiceResult<PaymentResponse>.Fail("Payment processing failed due to missing information.", HttpStatusCode.InternalServerError);
            }

            var paymentModel = new PaymentModel
            {
                OrderId = orderModel.OrderId,
                Amount = Convert.ToDecimal(payment.PaidPrice),
                PaymentDate = DateTime.UtcNow,
                Status = payment.Status,
                PaymentMethod = payment.CardType.ToString(),
                TransactionId = payment.PaymentId
            };

            await paymentRepository.AddAsync(paymentModel);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<PaymentResponse>.Success(new PaymentResponse(
                paymentModel.PaymentId,
                paymentModel.OrderId,
                paymentModel.Amount,
                paymentModel.Status,
                paymentModel.PaymentMethod,
                paymentModel.TransactionId,
                paymentModel.PaymentDate
            ));
        }

        public async Task<ServiceResult<PaymentResponse?>> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await paymentRepository.GetByIdAsync(paymentId);
            if (payment == null)
                return ServiceResult<PaymentResponse?>.Fail("Payment not found.");

            return ServiceResult<PaymentResponse?>.Success(MapToPaymentResponse(payment));
        }

        public async Task<ServiceResult<PaymentResponse?>> GetPaymentByOrderIdAsync(int orderId)
        {
            var payment = await paymentRepository.GetByOrderIdAsync(orderId);
            if (payment == null)
                return ServiceResult<PaymentResponse?>.Fail("Payment not found for the order.");

            return ServiceResult<PaymentResponse?>.Success(MapToPaymentResponse(payment));
        }

        public async Task<ServiceResult<IEnumerable<PaymentResponse>>> GetPaymentsByStatusAsync(string status)
        {
            var payments = await paymentRepository.GetPaymentsByStatusAsync(status);
            return ServiceResult<IEnumerable<PaymentResponse>>.Success(payments.Select(MapToPaymentResponse));
        }

        public async Task<ServiceResult<IEnumerable<PaymentResponse>>> GetPaymentsByDateAsync(DateTime startDate, DateTime endDate)
        {
            var payments = await paymentRepository.GetPaymentsByDateAsync(startDate, endDate);
            return ServiceResult<IEnumerable<PaymentResponse>>.Success(payments.Select(MapToPaymentResponse));
        }

        public async Task<ServiceResult<IEnumerable<PaymentResponse>>> GetPaymentsByOrderIdAsync(int orderId)
        {
            var payments = await paymentRepository.GetPaymentsByOrderIdAsync(orderId);
            if (payments == null)
                return ServiceResult<IEnumerable<PaymentResponse>>.Fail("Payments not found for the order.");

            return ServiceResult<IEnumerable<PaymentResponse>>.Success(payments.Select(MapToPaymentResponse));
        }

        public async Task<ServiceResult<bool>> CreatePaymentAsync(CreatePaymentModelRequest request)
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                return ServiceResult<bool>.Fail("Order not found.");

            var payment = new PaymentModel
            {
                OrderId = order.OrderId,
                Amount = request.Amount,
                Status = request.PaymentStatus,
                PaymentMethod = request.PaymentMethod,
                TransactionId = request.TransactionId,
                PaymentDate = DateTime.UtcNow
            };

            await paymentRepository.AddAsync(payment);
            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> UpdatePaymentStatusAsync(int paymentId, string status)
        {
            var payment = await paymentRepository.GetByIdAsync(paymentId);
            if (payment == null)
                return ServiceResult<bool>.Fail("Payment not found.");

            payment.Status = status;
            paymentRepository.Update(payment);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        private PaymentResponse MapToPaymentResponse(PaymentModel payment)
        {
            return new PaymentResponse(
                payment.PaymentId,
                payment.OrderId,
                payment.Amount,
                payment.Status.ToString(),
                payment.PaymentMethod,
                payment.TransactionId,
                payment.PaymentDate
            );
        }
    }
}
