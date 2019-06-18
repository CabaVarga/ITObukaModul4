using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Homework.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext()
        {
            Database.SetInitializer<DataAccessContext>(
                new DropCreateDatabaseIfModelChanges<DataAccessContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FileResource> FileResources { get; set; }
    }
}