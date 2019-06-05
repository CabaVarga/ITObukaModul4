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
using rep_uow.Models;
using rep_uow.Repositories;

namespace rep_uow.Controllers
{
    public class UsersController : ApiController
    {
        // DAC out, UOW in
        // private DataAccessContext db = new DataAccessContext();
        private UnitOfWork db = new UnitOfWork();


        // GET: api/Users
        // We have to change from IQueryable to IEnumerable because of UOW implementation
        public IEnumerable<User> GetUsers()
        {
            // DAC out UOW in
            // return db.Users;
            return db.UserRepository.Get();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            // DAC out UOW in
            /*
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
            */
            User user = db.UserRepository.GetByID(id);

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

            if (id != user.ID)
            {
                return BadRequest();
            }

            // DAC out UOW in
            /*
            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
            */

            db.UserRepository.Update(user);
            db.Save();

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

            // DAC out UOW in
            /*
            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
            */
            
            db.UserRepository.Insert(user);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            // DAC out UOW in
            /*
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
            */
            User user = db.UserRepository.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }

            db.UserRepository.Delete(id);
            db.Save();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*        private bool UserExists(int id)
                {
                    return db.Users.Count(e => e.ID == id) > 0;
                }
                */

        //[ResponseType(typeof(IEnumerable<User>))]
        //[Route("api/users/by-email")]
        //public IHttpActionResult GetUserByEmail([FromUri] string email)
        //{
        //    var found = db.UserRepository.Get(x => x.Email == email);
        //    return Ok(found);
        //}

        [Route("api/Users/by-email/{email}/")]
        [ResponseType(typeof(IEnumerable<User>))]
        public IHttpActionResult GetUserByEmail(string email)
        {
            IEnumerable<User> users = db.UserRepository.Get(filter: x => x.Email == email);

            // DILEMA: Sta je ispravno: vratiti praznu listu ili poruku da nema pronadjenih resursa?
            if (users.Count() == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [Route("api/Users/by-name")]
        [ResponseType(typeof(IEnumerable<User>))]
        public IHttpActionResult GetUsersOrderedByName()
        {
            IEnumerable<User> users = db.UserRepository.Get(orderBy: q => q.OrderBy(s => s.Name));

            // ISTA DILEMA KAO I KOD VRACANJA PO EMAIL-u
            if (users.Count() == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // 2.2 Omoguciti pronalazenje po datumu rodjenja
        // sortirano u rastucem redosledu imena
        [Route("api/Users/by-dob/{date}")]
        [ResponseType(typeof(IEnumerable<User>))]
        public IHttpActionResult GetUsersByDateOfBirth(DateTime date)
        {
            IEnumerable<User> users = db.UserRepository.Get(
                filter: u => u.DateOfBirth >= date, 
                orderBy: v => v.OrderBy(w => w.Name));

            if (users.Count() == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // 2.3 Omoguciti pronalazenje razlicitih imena korisnika po
        // prvom slovu imena
        [Route("api/Users/by-name-first-letter/{letter}")]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult GetUsersNamesByFirstLetter(string letter)
        {
            var names = db.UserRepository
                .Get(
                    filter: u => u.Name != null && u.Name.Substring(0,1) == letter,
                    orderBy: v => v.OrderBy(w => w.Name))
                .Select( x => x.Name );

            if (names.Count() == 0)
            {
                return NotFound();
            }

            return Ok(names);
        }
    }
}