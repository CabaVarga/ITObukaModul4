using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ServisniSlojBezServisa.Models;
using ServisniSlojBezServisa.Services;

namespace ServisniSlojBezServisa.Controllers
{
    public class UsersController : ApiController
    {
        private IUsersService usersService;
        /*private IUnitOfWork db;

        public UsersController(IUnitOfWork db)
        {
            this.db = db;
        }
        */
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return usersService.GetAllUsers();
            // return db.UsersRepository.Get();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = usersService.GetUser(id);
            // User user = db.UsersRepository.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
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

            User updatedUser =  usersService.UpdateUser(id, user.Name, user.Email);
            // db.UsersRepository.Update(user);
            // db.Save();

            if (updatedUser == null)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // db.UsersRepository.Insert(user);
            // db.Save();

            User createdUser = usersService.CreateUser(user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = usersService.DeleteUser(id);
            // User user = db.UsersRepository.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }

            // db.UsersRepository.Delete(id);
            // db.Save();

            return Ok(user);
        }

        // Added functionality
        [ResponseType(typeof(IEnumerable<User>))]
        [Route("api/users/by-name/{name}")]
        [HttpGet]
        public IHttpActionResult GetUsersByName (string name)
        {
            return Ok(usersService.GetUsersByName(name));
        }
    }
}