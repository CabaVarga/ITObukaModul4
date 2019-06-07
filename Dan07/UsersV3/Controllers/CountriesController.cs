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
using UsersV3.Models;
using UsersV3.Repositories;

namespace UsersV3.Controllers
{
    public class CountriesController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: api/Countries
        public IEnumerable<Country> GetCountries()
        {
            return db.CountryRepository.Get();
        }

        // GET: api/Countries/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountry(int id)
        {
            Country country = db.CountryRepository.GetByID(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.Id)
            {
                return BadRequest();
            }

            db.CountryRepository.Update(country);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Countries
        [ResponseType(typeof(Country))]
        public IHttpActionResult PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CountryRepository.Insert(country);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult DeleteCountry(int id)
        {
            Country country = db.CountryRepository.GetByID(id);
            if (country == null)
            {
                return NotFound();
            }

            db.CountryRepository.Delete(id);
            db.Save();

            return Ok(country);
        }
    }
}