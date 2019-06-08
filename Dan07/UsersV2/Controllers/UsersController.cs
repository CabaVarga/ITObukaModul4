using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using UsersV2.Models;
using UsersV2.Repositories;

namespace UsersV2.Controllers
{
    public class UsersController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return db.UserRepository.Get();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
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

            if (id != user.Id)
            {
                return BadRequest();
            }

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

            db.UserRepository.Insert(user);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.UserRepository.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }

            db.UserRepository.Delete(id);
            db.Save();

            return Ok(user);
        }


        // ZADATAK 2.2
        [ResponseType(typeof(User))]
        [Route("api/users/{id}/address/{addressId}", Name = "AddAddress")]
        [HttpPut]
        public IHttpActionResult PutAddress(int id, int addressId = 0)
        {
            User user = db.UserRepository.GetByID(id);

            if (user == null)
            {
                return NotFound();
            }

            Address address = db.AddressRepository.GetByID(addressId);

            if (address == null)
            {
                return NotFound();
            }

            user.Address = address;
            db.UserRepository.Update(user);
            db.Save(); // automatski ce biti sacuvana i adresa

            return CreatedAtRoute("AddAddress", new { id = user.Id, addressId = address.Id }, user);
        }

        // ZADATAK 2.3
        [HttpPost]
        [Route("api/Users/with-address")]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostNewUserWithAddress([FromBody] User user)
        {
            Address address = user.Address;

            List<Address> savedAddress = db.AddressRepository.Get(
                filter: a => a.Street == address.Street &&
                    a.City == address.City &&
                    a.Country == address.Country).ToList();

            /*
            if
            (db.AddressRepository.Get().Contains(address))
            {
                Debug.WriteLine("Address found in repo");
                db.UserRepository.Insert(user);
                db.Save();
                return Ok(user);
            }
            */

            // db.AddressRepository.Insert(address);
            if (savedAddress.Count != 0)
            {
                user.Address = savedAddress[0];
            }

            db.UserRepository.Insert(user);

            db.Save();
            return Ok(user);
        }
    }
}