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
using Homework.Services;

namespace Homework.Controllers
{
    public class AddressesController : ApiController
    {
        // private DataAccessContext db = new DataAccessContext();
        private IAddressesService addressesService;

        public AddressesController(IAddressesService addressesService)
        {
            this.addressesService = addressesService;
        }

        // GET: api/Addresses
        public IQueryable<Address> GetAddresses()
        {
            return addressesService.GetAllAddresses().AsQueryable();
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = addressesService.GetAddress(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            addressesService.CreateAddress(address);

            return CreatedAtRoute("DefaultApi", new { id = address.Id }, address);
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

            addressesService.UpdateAddress(id, address);

            return StatusCode(HttpStatusCode.NoContent);
        }
        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = addressesService.DeleteAddress(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }
    }
}