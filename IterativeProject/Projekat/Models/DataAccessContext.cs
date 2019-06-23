using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_3rd.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext() : base("DataAccesConnection")
        {
            Database.SetInitializer<DataAccessContext>(
                new DropCreateDatabaseIfModelChanges<DataAccessContext>());
        }

        public DbSet<UserModel> userModels { get; set; }
        public DbSet<CategoryModel> categoryModels { get; set; }
        public DbSet<OfferModel> offerModels { get; set; }
        public DbSet<BillModel> billModels { get; set; }
        public DbSet<VoucherModel> voucherModels { get; set; }

    }
}