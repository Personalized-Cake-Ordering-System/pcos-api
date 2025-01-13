
using CusCake.Domain.Entities;
using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CusCake.Infrastructures
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        #region  DB-Sets
        public DbSet<Customer> Customers { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        }
    }
}
