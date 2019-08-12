using OrdersDemo.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.Persistence
{
    public class OrdersDemoContext : DbContext
    {
        public OrdersDemoContext() : base()
        {
            Database.SetInitializer<OrdersDemoContext>(new DropCreateDatabaseIfModelChanges<OrdersDemoContext>());
        }

        public DbSet<User> userModels { get; set; }
        public DbSet<Category> categoryModels { get; set; }
        public DbSet<Offer> offerModels { get; set; }
        public DbSet<Bill> billModels { get; set; }
        public DbSet<Voucher> voucherModels { get; set; }
    }
}
