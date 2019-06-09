using dependency_injection_example.Models;
using dependency_injection_example.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dependency_injection_example.Controllers
{
    public class UsersController : ApiController
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public UsersController() { }


        // GET api/users
        public IEnumerable<User> GetActiveUsers()
        {
            return usersService.GetActiveUsers();
        }
    }
}
