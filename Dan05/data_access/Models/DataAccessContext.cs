using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace data_access.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext() : base("MySqlConnection")
        {
            Database.SetInitializer<DataAccessContext>(
                new DropCreateDatabaseIfModelChanges<DataAccessContext>());
        }
        public DbSet<User> Users { get; set; }
    }
}