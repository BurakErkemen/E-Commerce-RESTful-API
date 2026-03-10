using Microsoft.EntityFrameworkCore;
using System.Reflection;
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

namespace Web.Repository
{
    public class WebDbContext(DbContextOptions<WebDbContext> options) : DbContext(options)
    {
        public DbSet<UserModel> Users { get; set; } = default!;
        public DbSet<UserAddressModel> UserAddresses { get; set; } = default!;
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; } = default!;

        public DbSet<ProductModel> Products { get; set; } = default!;
        public DbSet<ReviewModel> Reviews { get; set; } = default!;
        public DbSet<CategoryModel> Categories { get; set; } = default!;

        public DbSet<WebSiteSettingsModel> WebSiteSettings { get; set; } = default!;
        public DbSet<BannerModel> Banners { get; set; } = default!;

        public DbSet<SupportTicketMessageModel> SupportTicketMessages { get; set; } = default!;
        public DbSet<SupportTicketModel> SupportTickets { get; set; } = default!;
        public DbSet<AttachmentModel> Attachments { get; set; } = default!;

        public DbSet<OrderModel> Orders { get; set; } = default!;
        public DbSet<BasketItemModel> BasketItems { get; set; } = default!;
        public DbSet<PaymentModel> Payments { get; set; } = default!;

        public DbSet<NotificationModel> NotificationModels { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
