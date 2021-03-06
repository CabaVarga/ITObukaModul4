﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using UsersV2.Models;
using UsersV2.Repositories;

namespace UsersV2.Controllers
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

            if (id != address.Id)
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

            return CreatedAtRoute("DefaultApi", new { id = address.Id }, address);
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

            db.AddressRepository.Delete(id);
            db.Save();

            return Ok(address);
        }
    }
}