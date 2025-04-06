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
        public DbSet<CakeDecorationOption> CakeDecorationOptions { get; set; }
        public DbSet<CakeDecorationSelection> CakeDecorationSelections { get; set; }
        // public DbSet<CakeDecorationType> CakeDecorationTypes { get; set; }
        // public DbSet<CakeExtraType> CakeExtraTypes { get; set; }
        public DbSet<CakeExtraOption> CakeExtraOptions { get; set; }
        public DbSet<CakeExtraSelection> CakeExtraSelections { get; set; }
        public DbSet<CakeMessageOption> CakeMessageOptions { get; set; }
        public DbSet<CakeMessageSelection> CakeMessageSelections { get; set; }
        // public DbSet<CakeMessageType> CakeMessageTypes { get; set; }
        public DbSet<CakePartOption> CakePartOptions { get; set; }
        // public DbSet<CakePartType> CakePartTypes { get; set; }
        public DbSet<CakePartSelection> CakePartSelections { get; set; }
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
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<Report> Reports { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        }
    }
}
