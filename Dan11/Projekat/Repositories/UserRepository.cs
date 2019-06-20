using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Projekat.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DataAccessContext context) : base(context) { }

        public IQueryable<User> GetSomeUsers()
        {
            var stuff = context.Users.
                Include(u => u.offerModels).
                Select(u => new
                {
                    u.last_name,
                    u.username
                }).ToList();

            return null;
        }
    }
}