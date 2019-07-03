using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_3rd_clean.Models;
using Project_3rd_clean.Models.DTOs.User;
using Project_3rd_clean.Repositories;
using Project_3rd_clean.Services;

namespace Project_3rd_clean.Controllers
{
    public class UserController : ApiController
    {
        private IUsersService userService;
        private IUnitOfWork db;

        public UserController(IUsersService userService, IUnitOfWork db)
        {
            this.userService = userService;
            this.db = db;
        }

        // GET: /project/users
        // ZADATAK 2.1.3
        [Route("project/users")]
        [HttpGet]
        public IEnumerable<User> GetUserModels()
        {
            return userService.GetAllUsers();
        }

        // GET: project/users/4
        // ZADATAK 2.1.4
        [Route("project/users/{id}", Name = "SingleUserById")]
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserModel(int id)
        {
            User userModel = userService.GetUser(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // PUT: project/users/5
        // ZADATAK 2.1.6
        [Route("project/users/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserModel(int id, User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userModel.id)
            {
                return BadRequest();
            }

            User updatedUser = userService.UpdateUser(id, userModel.first_name, userModel.last_name, userModel.username, userModel.email);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        // POST: project/users
        // ZADATAK 2.1.5
        [Route("project/users")]
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult PostUserModel(User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User createdUser = userService.CreateUser(userModel);

            // Mladen's solution
            // return Created("", createdUser);

            return CreatedAtRoute("SingleUserById", new { id = userModel.id }, userModel);
        }

        // DELETE: project/users/4
        // ZADATAK 2.1.9
        [Route("project/users/{id}")]
        [HttpDelete]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUserModel(int id)
        {
            User userModel = userService.DeleteUser(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // DONE: CRUD - 2.1.3 (get all), 2.1.4 (get one), 2.1.5 (add new), 2.1.6 (update one)
        // and 2.1.9 (delete one)
        // TODO: 2.1.7 (change role), 2.1.8 (change password), 2.1.9 (return by username)

        // PUT project/users/change/3/role/ROLE_ADMIN
        // ZADATAK 2.1.7
        [Route("project/users/change/{id}/role/{role}")]
        [HttpPut]
        [ResponseType(typeof(User))]
        public IHttpActionResult ChangeUserRoleForUser(int id, User.UserRoles role)
        {
            User userModel = userService.UpdateUserRole(id, role);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // PUT project/users/change/3/role/ROLE_ADMIN
        // ZADATAK 2.1.8
        [Route("project/users/changePassword/{id}")]
        [HttpPut]
        [ResponseType(typeof(User))]
        public IHttpActionResult ChangePasswordForUser(int id, [FromUri] string oldPassword, [FromUri] string newPassword)
        // another scheme, by using the URI: [FromUri]string oldPass, [FromUri]string newPass
        {
            User userModel = userService.UpdatePassword(id, oldPassword, newPassword);

            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(userModel);
        }

        // GET: project/users/by-username/blabla
        // ZADATAK 2.10
        [Route("project/users/by-username/{username}")]
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserByUsername(string username)
        {
            User user = userService.GetByUsername(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        #region PPA Get all users
        // GET project/users/public
        [Route("project/users/public")]
        [HttpGet]
        [ResponseType(typeof(PublicUserDTO))]
        #endregion
    }
}