using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Web.Service.Attachments;
using Web.Service.AuthServer;
using Web.Service.Banners;
using Web.Service.BasketItem;
using Web.Service.Categories;
using Web.Service.Orders;
using Web.Service.Payments;
using Web.Service.Products;
using Web.Service.Reviews;
using Web.Service.SupportTicketMessages;
using Web.Service.SupportTickets;
using Web.Service.UserAddress;
using Web.Service.Users;
using Web.Service.WebSiteSettings;

namespace Web.Service.Extensions
{
    public static class ServiceExtension
    {

        public static IServiceCollection AddServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserAddressService, UserAddressService>();
            services.AddScoped<ITokenHandler, TokenHandler>();

            services.AddScoped<IProductService , ProductService>();
            services.AddScoped<ICategoryService,  CategoryService>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<IWebSiteSettingsService, WebSiteSettingsService>();
            services.AddScoped<IBannerService, BannerService>();

            services.AddScoped<ISupportTicketService, SupportTicketService>();
            services.AddScoped<ISupportTicketMessageService, SupportTicketMessageService>();
            services.AddScoped<IAttachmentService, AttachmentService>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketItemService, BasketItemService>();
            
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
} 