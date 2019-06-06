﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Dan07.Models;
using Dan07.Repositories;

namespace Dan07.Controllers
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

            if (id != user.UserID)
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

            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
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

            db.UserRepository.Delete(user);
            db.Save();

            return Ok(user);
        }

        // ZADATAK 2.1
        // DELETE: api/Users/{userId}/delete-address/{addressId}
        [ResponseType(typeof(Address))]
        [HttpDelete]
        [Route("api/Users/{userId}/delete-address/{addressId}")]
        public IHttpActionResult DeleteAddressFromUser(int userId, int addressId)
        {
            // check if provided userId is valid
            User user = db.UserRepository.GetByID(userId);
            if (user == null)
            {
                return NotFound();
            }

            // check if provided addressId is valid AND is the Users address
            Address address = db.AddressRepository.GetByID(addressId);
            if (address == null || user.AddressID != addressId)
            {
                return NotFound();
            }

            user.AddressID = null;
            db.Save();
            foreach (var u in address.Users)
            {
                Debug.WriteLine("UserID: {0}, Name: {1}, AddressID: {2}", u.UserID, u.Name, u.AddressID);
            }
            
            return Ok(address);
        }



    }
}