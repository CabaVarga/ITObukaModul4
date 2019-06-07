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
    public class CitiesController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: api/Cities
        public IEnumerable<City> GetCities()
        {
            return db.CityRepository.Get();
        }

        // GET: api/Cities/5
        [ResponseType(typeof(City))]
        public IHttpActionResult GetCity(int id)
        {
            City city = db.CityRepository.GetByID(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Cities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCity(int id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.Id)
            {
                return BadRequest();
            }

            db.CityRepository.Update(city);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cities
        [ResponseType(typeof(City))]
        public IHttpActionResult PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CityRepository.Insert(city);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [ResponseType(typeof(City))]
        public IHttpActionResult DeleteCity(int id)
        {
            City city = db.CityRepository.GetByID(id);
            if (city == null)
            {
                return NotFound();
            }

            db.CityRepository.Delete(id);
            db.Save();

            return Ok(city);
        }
    }
}