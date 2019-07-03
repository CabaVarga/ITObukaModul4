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
using Homework.Utilities.Exceptions;

namespace Homework.Controllers
{
    [RoutePrefix("api/users")]
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
        [Route("")]
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> stuff = usersService.GetAllUsers();
            return stuff;
        }

        #region PPA Get all users
        [Route("public")]
        [HttpGet]
        public IEnumerable<PublicUserDTO> GetAllUsersPublic()
        {
            return usersService.GetAllUsers().Select(user =>
            {
                return DTOConverter.PublicUserDTO(user);
            });
        }

        [Route("private")]
        [HttpGet]
        public IEnumerable<PrivateUserDTO> GetAllUsersPrivate()
        {
            return usersService.GetAllUsers().Select(user =>
            {
                return DTOConverter.PrivateUserDTO(user);
            });
        }

        [Route("admin")]
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
        [Route("{id}", Name = "PrivateUserEndpoint")]
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
        [Route("public/{id}")]
        [ResponseType(typeof(PublicUserDTO))]
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
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            usersService.CreateUser(user);
                        
            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // PUT: api/Users/5
        [Route("")]
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
        [Route("{id}")]
        [HttpDelete]
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
        [Route("register")]
        [HttpPost]
        public IHttpActionResult PostRegisterUser(RegisterUserDTO newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request body is not in valid format.");
            }

            // If username or email exists new user can not be created
            // 1. Through DTOConverter

            // User user = DTOConverter.UserFromDTO(newUser);

            // usersService.CreateUser(Utilities.DTOConverter.UserDTO(newUser));
            // 2. Directly through Service
            try
            {
                User createdUser = usersService.CreateUser(newUser);

                return CreatedAtRoute("PrivateUserEndpoint", new { id = createdUser.Id }, DTOConverter.PublicUserDTO(createdUser));
            }
            catch (NameAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (EmailAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }
        #endregion

    }
}