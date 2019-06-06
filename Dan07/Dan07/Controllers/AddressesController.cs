using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Dan07.Models;
using Dan07.Repositories;

namespace Dan07.Controllers
{
    public class AddressesController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: api/Addresses
        public IEnumerable<Address> GetAddresses()
        {
            return db.AddressRepository.Get();
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = db.AddressRepository.GetByID(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressID)
            {
                return BadRequest();
            }

            db.AddressRepository.Update(address);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AddressRepository.Insert(address);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = address.AddressID }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = db.AddressRepository.GetByID(id);
            if (address == null)
            {
                return NotFound();
            }

            db.AddressRepository.Delete(address);
            db.Save();

            return Ok(address);
        }
        
        // ZADATAK 1.3
        // GET api/Addresses/by-city
        [Route("api/Addresses/by-city/{city}")]
        [ResponseType(typeof(IEnumerable<Address>))]
        public IHttpActionResult GetAddressesByCity(string city)
        {
            IEnumerable<Address> addresses = db.AddressRepository.Get(
                filter: a => a.City == city);

            return Ok(addresses);
        }

        // ZADATAK 1.4
        // GET api/Addresses/by-country
        [Route("api/Addresses/by-country")]
        [ResponseType(typeof(IEnumerable<Address>))]
        public IHttpActionResult GetAddressesSortedByCountry()
        {
            IEnumerable<Address> addresses = db.AddressRepository.Get(
                orderBy: a => a.OrderBy( b => b.Country));
             
            return Ok(addresses);
        }

        // ZADATAK 2.2 a
        // POST ili PUT? api/Addresses/{addressId}/add-user/{userId}
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/Addresses/{addressId}/add-user/{userId}")]
        public IHttpActionResult PutUserToAddressUsers(int addressId, int userId)
        {
            Address address = db.AddressRepository.GetByID(addressId);
            
            if (address == null)
            {
                return BadRequest();
            }

            User user = db.UserRepository.GetByID(userId);

            if (user == null)
            {
                return BadRequest();
            }

            // What to do if the User already has an associated address?
            // Should we just replace it from here?
            // TEST 1: user has no existing address
            // TEST 2: user has existing address
            // TEST: write to debug the User and Address data before and after the Update (PUT)
            address.Users.Add(user);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // ZADATAK 2.2 b
        // DELETE: api/Addresses/{addressId}/remove-user/{userId}
        [ResponseType(typeof(void))]
        [HttpDelete]
        [Route("api/Addresses/{addressId}/remove-user/{userId}")]
        public IHttpActionResult DeleteUserFromAddressUsers(int addressId, int userId)
        {
            Address address = db.AddressRepository.GetByID(addressId);

            if (address == null)
            {
                return BadRequest();
            }

            User user = db.UserRepository.GetByID(userId);

            if (user == null)
            {
                return BadRequest();
            }

            if (!address.Users.Contains(user))
            {
                return BadRequest();
            }
            
            address.Users.Remove(user);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}