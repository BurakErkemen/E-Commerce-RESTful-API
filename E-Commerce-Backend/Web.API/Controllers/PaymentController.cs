using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Service.Payments;
using Web.Service.Payments.Create;

namespace Web.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : CustomBaseController
    {

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPaymentAsync([FromBody] ProcessPaymentRequest request)
        {
            if (request == null || request.BasketItemIds == null || request.BasketItemIds.Count == 0)
            {
                return BadRequest("Invalid payment request.");
            }

            var result = await paymentService.ProcessPaymentAsync(request.BasketItemIds, request.UserId, request.CreatePaymentRequest);

            if (!result.IsSuccess)
            {
                return CreateActionResult(result); //StatusCode((int)result.StatusCode, result.ErrorMessage)
            }

            return CreateActionResult(result);
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentByIdAsync(int paymentId)
        {
            var result = await paymentService.GetPaymentByIdAsync(paymentId);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetPaymentByOrderIdAsync(int orderId)
        {
            var result = await paymentService.GetPaymentByOrderIdAsync(orderId);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetPaymentsByStatusAsync(string status)
        {
            var result = await paymentService.GetPaymentsByStatusAsync(status);

            return Ok(result.Data);
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetPaymentsByDateAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await paymentService.GetPaymentsByDateAsync(startDate, endDate);

            return Ok(result.Data);
        }

        [HttpGet("order-payments/{orderId}")]
        public async Task<IActionResult> GetPaymentsByOrderIdAsync(int orderId)
        {
            var result = await paymentService.GetPaymentsByOrderIdAsync(orderId);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] CreatePaymentModelRequest request)
        {
            var result = await paymentService.CreatePaymentAsync(request);
            if (!result.IsSuccess)
            {
                return CreateActionResult(result);
            }

            return Ok(result.Data);
        }

        [HttpPut("update-status/{paymentId}")]
        public async Task<IActionResult> UpdatePaymentStatusAsync(int paymentId, [FromBody] UpdatePaymentStatusRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Status))
            {
                return BadRequest("Invalid payment status update request.");
            }

            var result = await paymentService.UpdatePaymentStatusAsync(paymentId, request.Status);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
    public class ProcessPaymentRequest
    {
        public required List<int> BasketItemIds { get; set; }
        public int UserId { get; set; }
        public required CreatePaymentRequest CreatePaymentRequest { get; set; }
    }

    public class UpdatePaymentStatusRequest
    {
        public required string Status { get; set; }
    }