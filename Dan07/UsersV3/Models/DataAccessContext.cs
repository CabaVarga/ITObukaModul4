﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UsersV3.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext() : base("DataAccessConnection")
        {
            Database.SetInitializer<DataAccessContext>(
                new DropCreateDatabaseIfModelChanges<DataAccessContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}