﻿using System;
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
using Projekat.Models;
using Projekat.Repositories;

namespace Projekat.Controllers
{
    public class UserController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: /project/users
        // ZADATAK 2.1.3
        [Route("project/users")]
        [HttpGet]
        public IEnumerable<User> GetuserModels()
        {
            return db.UsersRepository.Get();
        }

        // GET: project/users/4
        // ZADATAK 2.1.4
        [Route("project/users/{id}")]
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserModel(int id)
        {
            User userModel = db.UsersRepository.GetByID(id);
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
                Debug.WriteLine("Invalid ModelState at PutUserModel");
                return BadRequest(ModelState);
            }

            if (id != userModel.id)
            {
                Debug.WriteLine("Invalid ModelState at PutUserModel, id not found...! OK, we are using some other sutf");
                return BadRequest();
            }

            // Simply for trying out why my commits do not show up....

            // ZAHTEV: ne menjati user_role ni password
            User savedUser = db.UsersRepository.GetByID(id);

            string savedPassword = db.UsersRepository.GetByID(id).password;
            User.UserRoles savedRole = db.UsersRepository.GetByID(id).user_role;
            // Let's try this one out
            userModel.password = savedPassword;
            userModel.user_role = savedRole;

            // lets try brute force...
            savedUser.first_name = userModel.first_name;
            savedUser.last_name = userModel.last_name;
            savedUser.email = userModel.email;
            savedUser.username = userModel.username;

            // savedUser = userModel;
            // db.UserRepository.Update(userModel);
            db.UsersRepository.Update(savedUser);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
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
                Debug.WriteLine("Invalid ModelState at PostUserModel");
                return BadRequest(ModelState);
            }

            // OVO se zahteva u zadatku 2.1.5
            userModel.user_role = Models.User.UserRoles.ROLE_CUSTOMER;

            db.UsersRepository.Insert(userModel);
            db.Save();

            return CreatedAtRoute("SingleUserById", new { id = userModel.id }, userModel);
        }

        // DELETE: project/users/4
        // ZADATAK 2.1.9
        [Route("project/users/{id}", Name = "SingleUserById")]
        [HttpDelete]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUserModel(int id)
        {
            User userModel = db.UsersRepository.GetByID(id);
            if (userModel == null)
            {
                return NotFound();
            }

            db.UsersRepository.Delete(userModel);
            db.Save();

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
            User userModel = db.UsersRepository.GetByID(id);
            if (userModel == null)
            {
                return NotFound();
            }

            userModel.user_role = role;
            db.Save();

            return Ok(userModel);

            // return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT project/users/change/3/role/ROLE_ADMIN
        // ZADATAK 2.1.8
        [Route("project/users/changePassword/{id}")]
        [HttpPut]
        [ResponseType(typeof(User))]
        public IHttpActionResult ChangePasswordForUser(int id, [FromBody]Dictionary<string, string> oldNewPass)
        // another scheme, by using the URI: [FromUri]string oldPass, [FromUri]string newPass
        {
            User userModel = db.UsersRepository.GetByID(id);
            if (userModel == null)
            {
                return NotFound();
            }

            if (userModel.password != oldNewPass["oldPass"])
            {
                return BadRequest("Wrong password!");
            }

            userModel.password = oldNewPass["newPass"];
            db.Save();

            return Ok(userModel);

            // return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: project/users/by-username/blabla
        // ZADATAK 2.10
        [Route("project/users/by-username/{username}")]
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserByUsername(string username)
        {
            List<User> userModel = db.UsersRepository.Get(
                filter: u => u.username == username).ToList();

            if (userModel.Count == 0)
            {
                return NotFound();
            }

            return Ok(userModel[0]);
        }
    }
}