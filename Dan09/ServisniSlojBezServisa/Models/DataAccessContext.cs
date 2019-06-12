using System.Data.Entity;

namespace ServisniSlojBezServisa.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext() : base("DataAccessConnection")
        {
            Database.SetInitializer<DataAccessContext>(
                new DropCreateDatabaseIfModelChanges<DataAccessContext>());
        }

        public DbSet<User> Users { get; set; }
    }
}