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
using radproj_v1.Models;

namespace radproj_v1.Controllers
{
    public class RadniksController : ApiController
    {
        private RadProjContext db = new RadProjContext();

        // GET: api/Radniks
        public IQueryable<Radnik> GetRadnici()
        {
            return db.Radnici;
        }

        // GET: api/Radniks/5
        [ResponseType(typeof(Radnik))]
        public IHttpActionResult GetRadnik(long id)
        {
            Radnik radnik = db.Radnici.Find(id);
            if (radnik == null)
            {
                return NotFound();
            }

            return Ok(radnik);
        }

        // PUT: api/Radniks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRadnik(long id, Radnik radnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != radnik.MaticniBroj)
            {
                return BadRequest();
            }

            db.Entry(radnik).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RadnikExists(id))
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

        // POST: api/Radniks
        [ResponseType(typeof(Radnik))]
        public IHttpActionResult PostRadnik(Radnik radnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Radnici.Add(radnik);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = radnik.MaticniBroj }, radnik);
        }

        // DELETE: api/Radniks/5
        [ResponseType(typeof(Radnik))]
        public IHttpActionResult DeleteRadnik(long id)
        {
            Radnik radnik = db.Radnici.Find(id);
            if (radnik == null)
            {
                return NotFound();
            }

            db.Radnici.Remove(radnik);
            db.SaveChanges();

            return Ok(radnik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RadnikExists(long id)
        {
            return db.Radnici.Count(e => e.MaticniBroj == id) > 0;
        }
    }
}