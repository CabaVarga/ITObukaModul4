using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Homework.Models;
using Homework.Models.DTOs.User;
using Homework.Repositories;
using Homework.Services;
using Homework.Utilities;

namespace Homework.Controllers
{
    public class UsersController : ApiController
    {
        // private DataAccessContext db = new DataAccessContext();
        // private IUnitOfWork db;
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }


        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return usersService.GetAllUsers();
        }

        #region PPA Get all users
        [Route("api/users/public")]
        [HttpGet]
        public IEnumerable<PublicUserDTO> GetAllUsersPublic()
        {
            return usersService.GetAllUsers().Select(user =>
            {
                return DTOConverter.PublicUserDTO(user);
            });
        }

        [Route("api/users/private", Name = "PrivateUserEndpoint")]
        [HttpGet]
        public IEnumerable<PrivateUserDTO> GetAllUsersPrivate()
        {
            return usersService.GetAllUsers().Select(user =>
            {
                return DTOConverter.PrivateUserDTO(user);
            });
        }

        [Route("api/users/admin")]
        [HttpGet]
        public IEnumerable<AdminUserDTO> GetAllUsersAdmin()
        {
            return usersService.GetAllUsers().Select(user =>
            {
                return DTOConverter.AdminUserDTO(user);
            });
        }
        #endregion

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = usersService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        #region PPA Get a single user by ID

        // GET api/users/public/1
        [Route("api/users/public/{id}")]
        [ResponseType(typeof(PrivateUserDTO))]
        [HttpGet]
        public IHttpActionResult GetUserPublic(int id)
        {
            User user = usersService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(DTOConverter.PublicUserDTO(user));

            // this would also work
            // return Ok(new PublicUserDTO() { Id = user.Id, Name = user.Name });
            // another approach is to return DTO directly from the service
        }
        #endregion


        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usersService.CreateUser(user);

            // The CreatedAtRoute will not show the real Id ...
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            User updatedUser = usersService.UpdateUser(id, user);

            if (updatedUser == null)
            {
                return NotFound();
            }
           
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = usersService.DeleteUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        #region Register new user
        // POST api/users/register
        [Route("api/users/register")]
        [HttpPost]
        public IHttpActionResult PostRegisterUser(RegisterUserDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request body is not in valid format.");
            }

            // If username or email exists new user can not be created
            // 1. Through DTOConverter
            // usersService.CreateUser(Utilities.DTOConverter.UserDTO(newUser));
            // 2. Directly through Service
            try
            {
                User createdUser = usersService.CreateUser(newUser);

                return CreatedAtRoute("PrivateUserEndpoint", new { id = createdUser.Id }, DTOConverter.PublicUserDTO(createdUser));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

    }
}