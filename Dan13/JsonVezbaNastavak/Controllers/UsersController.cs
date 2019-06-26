using JsonVezbaNastavak.Models;
using JsonVezbaNastavak.Models.DTOs;
using JsonVezbaNastavak.Models.DTOs.Address;
using JsonVezbaNastavak.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JsonVezbaNastavak.Controllers
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
                user.AccessType = EAccessType.Public;
                user.Address.Access = EAccessType.Public;
                return user;
            });
        }

        [Route("api/users/private")]
        public IEnumerable<PrivateUserDTO> GetAllPrivate()
        {
            return GetDummyDB().Select(user =>
            {
                //user.AccessType = EAccessType.Private;
                //user.Address.Access = EAccessType.Private;
                PrivateUserDTO userDTO = new PrivateUserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Address = new PrivateAddressDTO()
                    {
                        Id = user.Address.Id,
                        Street = user.Address.Street,
                        City = user.Address.City,
                        Country = user.Address.Country
                    }
                };

                return userDTO;
            });
        }

        [Route("api/users/admin")]
        public IEnumerable<PublicUserDTO> GetAllAdmin()
        {
            return GetDummyDB().Select(user =>
            {
                //user.AccessType = EAccessType.Admin;
                //user.Address.Access = EAccessType.Admin;
                PublicUserDTO userDTO = new PublicUserDTO();
                userDTO.Id = user.Id;
                userDTO.Name = user.Name;

                return userDTO;
            });
        }


        //public HttpResponseMessage Get(int id)
        //{
        //    try
        //    {
        //        if (id == 5)
        //        {
        //            throw new Exception("User doesn't exist");
        //        }

        //        var user = GetDummyDB().FirstOrDefault(x => x.Id == id);

        //        if (user == null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound);
        //        }

        //        user.AccessType = EAccessType.Admin;
        //        user.Address.Access = EAccessType.Admin;

        //        return Request.CreateResponse(HttpStatusCode.OK, user);
        //    } catch (Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}


        public IHttpActionResult Get(int id)
        {
            try
            {
                if (id == 5)
                {
                    throw new Exception("User doesn't exist");
                }

                var user = GetDummyDB().FirstOrDefault(x => x.Id == id);

                if (user == null)
                {
                    return NotFound(); // Request.CreateResponse(HttpStatusCode.NotFound);
                }

                //user.AccessType = EAccessType.Admin;
                //user.Address.Access = EAccessType.Admin;

                return Ok(user); // Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception e)
            {
                return InternalServerError(e); // Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        // Zadatak tj primer sa strane 91
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage PostRegisterUser([FromBody]UserRegisterDTO user)
        {
            User ue = new User();
            ue.Id = 3;
            ue.Name = user.Name;
            ue.Email = user.Email;
            //ue.AccessType = EAccessType.Admin; // We don't have AccessType anymore.

            return Request.CreateResponse(HttpStatusCode.Accepted, ue);
        }

    }
}
