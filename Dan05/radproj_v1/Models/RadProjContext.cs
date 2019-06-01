using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace radproj_v1.Models
{
    public class RadProjContext : DbContext
    {
        public RadProjContext() : base("radproj_v1Connection")
        {
            Database.SetInitializer<RadProjContext>(
                new DropCreateDatabaseIfModelChanges<RadProjContext>()
            );
        }

        public DbSet<Radnik> Radnici { get; set; }
    }
}