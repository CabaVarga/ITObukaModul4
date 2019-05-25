using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Controllers
{
    public class UserController : ApiController
    {
        private List<UserModel> GetDB() {
            List<UserModel> users = new List<UserModel>
            {
                new UserModel
                {
                    id = 11,
                    firstName = "Jovan",
                    lastName = "Ducic",
                    userName = "jovanducic",
                    password = "jovanducic",
                    email = "jd@gmail.com",
                    userRole = UserRole.ROLE_ADMIN
                },
                new UserModel
                {
                    id = 12,
                    firstName = "Jovan",
                    lastName = "Milosevic",
                    userName = "jovanmilosevic",
                    password = "jovanmilosevic",
                    email = "jm@gmail.com",
                    userRole = UserRole.ROLE_CUSTOMER
                },
                new UserModel
                {
                    id = 13,
                    firstName = "Momir",
                    lastName = "Milosevic",
                    userName = "momirmilosevic",
                    password = "momirmilosevic",
                    email = "mm@gmail.com",
                    userRole = UserRole.ROLE_CUSTOMER
                },
                new UserModel
                {
                    id = 14,
                    firstName = "Marija",
                    lastName = "Ducic",
                    userName = "marijaducic",
                    password = "marijaducic",
                    email = "md@gmail.com",
                    userRole = UserRole.ROLE_SELLER
                },
                new UserModel
                {
                    id = 15,
                    firstName = "Marko",
                    lastName = "Jovic",
                    userName = "markojovic",
                    password = "markojovic",
                    email = "mj@gmail.com",
                    userRole = UserRole.ROLE_ADMIN
                },
                new UserModel
                {
                    id = 16,
                    firstName = "Marko",
                    lastName = "Ducic",
                    userName = "markoducic",
                    password = "markonducic",
                    email = "md@gmail.com",
                    userRole = UserRole.ROLE_SELLER
                }
            };
            return users;
        }

        // Zadatak 1.3
        [Route("project/users")]
        [HttpGet]
        public List<UserModel> GetAllUsers()
        {
            return GetDB();
        }

        // Zadatak 1.4
        [Route("project/users/{id}")]
        [HttpGet]
        public UserModel GetUser(int id)
        {
            return GetDB().Find(x => x.id == id);
        }

        // Zadatak 1.5
        [Route("project/users")]
        [HttpPost]
        public UserModel PostNewUser([FromBody]UserModel user)
        {
            List<UserModel> users = GetDB();
            users.Add(new UserModel
            {
                id = user.id,
                firstName = user.firstName,
                lastName = user.lastName,
                userName = user.userName,
                password = user.password,
                email = user.email,
                userRole = UserRole.ROLE_CUSTOMER
            });

            return users.Find(x => x.id == user.id);
        }

        // Zadatak 1.6
        [Route("project/users/{id}")]
        [HttpPut]
        public UserModel PutChangeUserData(int id, [FromBody]UserModel user)
        {
            List<UserModel> users = GetDB();
            UserModel userExisting = users.Find(x => x.id == id);

            if (userExisting != null)
            {
                userExisting.firstName = (user.firstName != null) ? user.firstName : userExisting.firstName;
                userExisting.lastName = (user.lastName != null) ? user.lastName : userExisting.lastName;
                userExisting.userName = (user.userName != null) ? user.userName : userExisting.userName;
                userExisting.email = (user.email != null) ? user.email : userExisting.email;
            }

            return users.Find(x => x.id == id);
            // return userExisting;
        }

        // Zadatak 1.7
        [Route("project/users/change/{id}/role/{role}")]
        [HttpPut]
        public UserModel PutChangeUserRole(int id, UserRole role)
        {
            UserModel user = GetDB().Find(x => x.id == id);
            if (user != null)
            {
                user.userRole = role;
            }

            return user;
        }

        // Zadatak 1.8
        [Route("project/users/changePassword/{id}")]
        [HttpPut]
        public UserModel PutChangeUserPassword(int id, [FromBody]Dictionary<string, string> oldNewPass)
        {
            UserModel user = GetDB().Find(x => x.id == id);
            if (user != null)
            {
                if (user.password == oldNewPass["oldPass"])
                {
                    user.password = oldNewPass["newPass"];
                }
            }

            return user;
        }

        // Zadatak 1.9
        [Route("project/users/{id}")]
        [HttpDelete]
        public UserModel DeleteUser(int id)
        {
            List<UserModel> users = GetDB();
            UserModel user = users.Find(x => x.id == id);
            if (user != null)
            {
                users.Remove(user);
            }

            return user;
        }

        // 1.10
        [Route("project/users/by-username/{username}")]
        [HttpGet]
        public UserModel GetUserByUsername(string username)
        {
            // Ako je username u formatu caba.varga, metod nece raditi...
            return GetDB().Find(x => x.userName == username);
        }
    }
}
