using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Project_3rd.Repositories
{
    public class UserRepository : GenericRepository<UserModel>
    {
        public UserRepository(DataAccessContext context) : base(context) { }

        public IQueryable<UserModel> GetSomeUsers()
        {
            var stuff = context.userModels.
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