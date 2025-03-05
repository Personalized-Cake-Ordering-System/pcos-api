using CusCake.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CusCake.Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        #region  DB-Sets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Bakery> Bakeries { get; set; }
        public DbSet<AvailableCake> AvailableCakes { get; set; }
        public DbSet<BankEvent> BankEvents { get; set; }
        public DbSet<CakeDecoration> CakeDecorations { get; set; }
        public DbSet<CakeDecorationDetail> CakeDecorationDetails { get; set; }
        public DbSet<CakeExtra> CakeExtras { get; set; }
        public DbSet<CakeExtraDetail> CakeExtraDetails { get; set; }
        public DbSet<CakeMessage> CakeMessages { get; set; }
        public DbSet<CakeMessageDetail> CakeMessageDetails { get; set; }
        public DbSet<CakeMessageType> CakeMessageTypes { get; set; }
        public DbSet<CakePart> CakeParts { get; set; }
        public DbSet<CakePartDetail> CakePartDetails { get; set; }
        public DbSet<CakeReview> CakeReviews { get; set; }
        public DbSet<CustomCake> CustomCakes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderSupport> OrderSupports { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<CustomerVoucher> CustomerVouchers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        }
    }
}
