using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Homework.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext() : base("name=DataAccessConnection")
        {
            // HERE: https://stackoverflow.com/a/42874541/4486196
            // and for the line: https://www.entityframeworktutorial.net/lazyloading-in-entity-framework.aspx
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}