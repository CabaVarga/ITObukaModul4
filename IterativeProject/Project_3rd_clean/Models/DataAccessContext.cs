using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext() : base("name=DataAccessConnection")
        {
            Database.SetInitializer<DataAccessContext>(new ContextSeeder());
        }

        public DbSet<User> userModels { get; set; }
        public DbSet<Category> categoryModels { get; set; }
        public DbSet<Offer> offerModels { get; set; }
        public DbSet<Bill> billModels { get; set; }
        public DbSet<Voucher> voucherModels { get; set; }

    }
}