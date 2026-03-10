using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Repository.Admin.Banner;
using Web.Repository.Admin.Notifications;
using Web.Repository.Admin.WebSiteSettings;
using Web.Repository.OrderSection.BasketItem;
using Web.Repository.OrderSection.Order;
using Web.Repository.OrderSection.Payment;
using Web.Repository.ProductInfo.Categories;
using Web.Repository.ProductInfo.Products;
using Web.Repository.ProductInfo.Reviews;
using Web.Repository.TicketModels.Attachments;
using Web.Repository.TicketModels.SupportTicketMessages;
using Web.Repository.TicketModels.SupportTickets;
using Web.Repository.UserInfo.Token;
using Web.Repository.UserInfo.UserAddresses;
using Web.Repository.UserInfo.Users;

namespace Web.Repository.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<WebDbContext>(options =>
            {
                var connectionStringOption = configuration.GetSection
                (ConnectionStringOption.Key).Get<ConnectionStringOption>();

                options.UseSqlServer(connectionStringOption!.SqlServer,sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof
                        (RepositoryAssembly).Assembly.FullName);
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAddressRepository,UserAddressRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            #region Product-Category-Review
            services.AddScoped<IProductRepository , ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            #endregion

            #region WebSite Setting - Banner
            services.AddScoped<IWebSiteSettingsRepository, WebSiteSettingsRepository>();
            services.AddScoped<IBannerRepository , BannerRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            #endregion

            #region Tickets 
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
            services.AddScoped<ISupportTicketMessageRepository, SupportTicketMessageRepository>();
            #endregion

            #region Order-Payment
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            #endregion

            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IUnitOFWork, UnitOFWork>();
            return services;
        }
    }
}
