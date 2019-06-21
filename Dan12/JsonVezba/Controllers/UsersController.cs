using JsonVezba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JsonVezba.Controllers
{
    public class UsersController : ApiController
    {
        private IEnumerable<User> GetDummyDB()
        {
            List<User> users = new List<User>();

            var a = new Address();
            a.City = "Novi Sad";
            a.Country = "Serbia";
            a.Street = "Gundiliceva";
            a.Id = 1;

            var u1 = new User();
            u1.Id = 1;
            u1.Name = "Ivan";
            u1.Email = "ivan@example.com";
            u1.DateOfBirth = new DateTime(1988, 3, 5);
            u1.Address = a;
            u1.Password = "password";

            var u2 = new User();
            u2.Id = 2;
            u2.Name = "Mladen";
            u2.Email = "mladen@example.com";
            u2.DateOfBirth = new DateTime(1985, 3, 5);
            u2.Address = a;
            u2.Password = "password";

            users.Add(u1);
            users.Add(u2);

            a.Users.Add(u1);
            a.Users.Add(u2);

            return users;
        }

        [Route("api/users/public")]
        public IEnumerable<User> GetAllPublic()
        {
            return GetDummyDB().Select(user =>
            {
                user.AccesType = EAccesType.Public;
                user.Address.AccesType = EAccesType.Public;
                return user;
            });
        }

        [Route("api/users/private")]
        public IEnumerable<User> GetAllPrivate()
        {
            return GetDummyDB().Select(user =>
            {
                user.AccesType = EAccesType.Private;
                user.Address.AccesType = EAccesType.Private;
                return user;
            });
        }

        [Route("api/users/admin")]
        public IEnumerable<User> GetAllAdmin()
        {
            return GetDummyDB().Select(user =>
            {
                user.AccesType = EAccesType.Admin;
                user.Address.AccesType = EAccesType.Admin;
                return user;
            });
        }
    }
}
