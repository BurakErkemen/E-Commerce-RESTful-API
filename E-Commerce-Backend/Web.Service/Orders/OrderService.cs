using Web.Repository;
using Web.Repository.OrderSection.BasketItem;
using Web.Repository.OrderSection.Order;
using Web.Service.Orders.Create;
using Web.Service.Payments;

namespace Web.Service.Orders
{
    public class OrderService(
        IOrderRepository orderRepository,
        IBasketItemRepository basketItemRepository,
        IPaymentService paymentService,
        IUnitOFWork unitOFWork) : IOrderService
    {
        // Sipariş oluşturma
        public async Task<ServiceResult<CreateOrderResponse>> CreateOrderAsync(CreateOrderRequest request)
        {
            var basketItems = await basketItemRepository.GetBasketItemsByUserIdAsync(request.UserId);
            if (basketItems is null || !basketItems.Any())
                return ServiceResult<CreateOrderResponse>.Fail("Basket is empty.",System.Net.HttpStatusCode.NotFound);

            var order = new OrderModel
            {
                UserId = request.UserId,
                TotalAmount = request.TotalAmount,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            await orderRepository.AddAsync(order);
            await unitOFWork.SaveChangesAsync();
            // Ödeme işlemi
            var paymentResult = await paymentService.GetPaymentByOrderIdAsync(order.OrderId);

            if (!paymentResult.IsSuccess || paymentResult.Data == null)
            {
                order.Status = "Fail";
                orderRepository.Update(order);
                await unitOFWork.SaveChangesAsync();
                ServiceResult<CreateOrderResponse>.Fail("Payment failed.",System.Net.HttpStatusCode.BadRequest);
            }

            // Ödeme başarılıysa
            order.Status = "Completed";
            orderRepository.Update(order);
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<CreateOrderResponse>.Success(new CreateOrderResponse(order.OrderId));
        }

        public async Task<ServiceResult<bool>> UpdateOrderAsync(int orderId, string newStatus)
        {
            var order = await orderRepository.GetByIdDetailAsync(orderId);
            if (order == null)
                return ServiceResult<bool>.Fail("Order not found.");

            order.Status = newStatus;
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> CancelOrderAsync(int orderId)
        {
            var order = await orderRepository.GetByIdDetailAsync(orderId);
            if (order == null)
                return ServiceResult<bool>.Fail("Order not found.");

            if (order.Status == "Completed")
                return ServiceResult<bool>.Fail("Completed orders cannot be canceled.");

            order.Status = "Cancelled";
            await unitOFWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<OrderResponse>> GetOrderDetailsAsync(int orderId)
        {
            var order = await orderRepository.GetByIdDetailAsync(orderId);
            if (order == null)
                return ServiceResult<OrderResponse>.Fail("Order not found.");

            var response = new OrderResponse(
                order.OrderId,
                order.UserId,
                order.TotalAmount,
                order.Status,
                order.OrderDate
            );

            return ServiceResult<OrderResponse>.Success(response);
        }
    }
}