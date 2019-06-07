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
using Projekat.Models;

namespace Projekat.Controllers
{
    public class UserController : ApiController
    {
        private DataAccessContext db = new DataAccessContext();

        // GET: /project/users
        // ZADATAK 2.1.3
        [Route("project/users")]
        [HttpGet]
        public IQueryable<UserModel> GetuserModels()
        {
            return db.userModels;
        }

        // GET: project/users/4
        // ZADATAK 2.1.4
        [Route("project/users/{id}")]
        [HttpGet]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUserModel(int id)
        {
            UserModel userModel = db.userModels.Find(id);
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
        public IHttpActionResult PutUserModel(int id, UserModel userModel)
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

            // ZAHTEV: ne menjati user_role ni password
            UserModel savedUser = db.userModels.Find(id);
            // Let's try this one out
            db.Entry(savedUser).State = EntityState.Detached;

            userModel.password = savedUser.password;
            userModel.user_role = savedUser.user_role;

            db.Entry(userModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: project/users
        // ZADATAK 2.1.5
        [Route("project/users")]
        [ResponseType(typeof(UserModel))]
        [HttpPost]
        public IHttpActionResult PostUserModel(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Invalid ModelState at PostUserModel");
                return BadRequest(ModelState);
            }

            // OVO se zahteva u zadatku 2.1.5
            userModel.user_role = UserModel.UserRoles.ROLE_CUSTOMER;

            db.userModels.Add(userModel);
            db.SaveChanges();

            return CreatedAtRoute("SingleUserById", new { id = userModel.id }, userModel);
        }

        // DELETE: project/users/4
        // ZADATAK 2.1.9
        [Route("project/users/{id}", Name = "SingleUserById")]
        [HttpDelete]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult DeleteUserModel(int id)
        {
            UserModel userModel = db.userModels.Find(id);
            if (userModel == null)
            {
                return NotFound();
            }

            db.userModels.Remove(userModel);
            db.SaveChanges();

            return Ok(userModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserModelExists(int id)
        {
            return db.userModels.Count(e => e.id == id) > 0;
        }

        // DONE: CRUD - 2.1.3 (get all), 2.1.4 (get one), 2.1.5 (add new), 2.1.6 (update one)
        // and 2.1.9 (delete one)
        // TODO: 2.1.7 (change role), 2.1.8 (change password), 2.1.9 (return by username)

        // PUT project/users/change/3/role/ROLE_ADMIN
        // ZADATAK 2.1.7
        [Route("project/users/change/{id}/role/{role}")]
        [HttpPut]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult ChangeUserRoleForUser(int id, UserModel.UserRoles role)
        {
            UserModel userModel = db.userModels.Find(id);
            if (userModel == null)
            {
                return NotFound();
            }

            userModel.user_role = role;
            db.SaveChanges();

            return Ok(userModel);

            // return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT project/users/change/3/role/ROLE_ADMIN
        // ZADATAK 2.1.8
        [Route("project/users/changePassword/{id}")]
        [HttpPut]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult ChangePasswordForUser(int id, [FromBody]Dictionary<string, string> oldNewPass)
        // another scheme, by using the URI: [FromUri]string oldPass, [FromUri]string newPass
        {
            UserModel userModel = db.userModels.Find(id);
            if (userModel == null)
            {
                return NotFound();
            }

            if (userModel.password != oldNewPass["oldPass"])
            {
                return BadRequest("Wrong password!");
            }

            userModel.password = oldNewPass["newPass"];
            db.SaveChanges();

            return Ok(userModel);

            // return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: project/users/by-username/blabla
        // ZADATAK 2.10
        [Route("project/users/by-username/{username}")]
        [HttpGet]
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUserByUsername(string username)
        {
            List<UserModel> userModel = db.userModels.Where(u => u.username == username).ToList();
            if (userModel.Count == 0)
            {
                return NotFound();
            }

            return Ok(userModel[0]);
        }
    }
}