using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projekat.Models
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
    }
}