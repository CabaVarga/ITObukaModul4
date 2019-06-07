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
using Projekat.Models;

namespace Projekat.Controllers
{
    public class OfferController : ApiController
    {
        private DataAccessContext db = new DataAccessContext();

        // GET: project/offers
        // ZADATAK 2.3.3
        [Route("project/offers")]
        [HttpGet]
        public IQueryable<OfferModel> GetofferModels()
        {
            return db.offerModels;
        }

        // GET: project/offers/3
        // ZADATAK 2.3.7
        [HttpGet]
        [Route("project/offers/{id}", Name = "SingleOfferById")]
        [ResponseType(typeof(OfferModel))]
        public IHttpActionResult GetOfferModel(int id)
        {
            OfferModel offerModel = db.offerModels.Find(id);
            if (offerModel == null)
            {
                return NotFound();
            }

            return Ok(offerModel);
        }

        // PUT: project/offers/3
        // ZADATAK 2.3.5
        [Route("project/offers/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfferModel(int id, OfferModel offerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offerModel.id)
            {
                return BadRequest();
            }


            // HMMM something is fishy here... 
            // btw, it'll throw an exception if savedModel is null hehe
            OfferModel savedModel = db.offerModels.Find(id);
            db.Entry(savedModel).State = EntityState.Detached;

            offerModel.offer_status = savedModel.offer_status;
            db.Entry(offerModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferModelExists(id))
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

        // POST: project/offers
        // ZADATAK 2.3.4
        [Route("project/offers")]
        [HttpPost]
        [ResponseType(typeof(OfferModel))]
        public IHttpActionResult PostOfferModel(OfferModel offerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.offerModels.Add(offerModel);
            db.SaveChanges();

            return CreatedAtRoute("SingleOfferById", new { id = offerModel.id }, offerModel);
        }

        // DELETE: project/offers/3
        // ZADATAK 2.3.6
        [Route("project/offers/{id}")]
        [HttpDelete]
        [ResponseType(typeof(OfferModel))]
        public IHttpActionResult DeleteOfferModel(int id)
        {
            OfferModel offerModel = db.offerModels.Find(id);
            if (offerModel == null)
            {
                return NotFound();
            }

            db.offerModels.Remove(offerModel);
            db.SaveChanges();

            return Ok(offerModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfferModelExists(int id)
        {
            return db.offerModels.Count(e => e.id == id) > 0;
        }

        // PUT: project/offers/changeOffer/3/status/WAIT_FOR_APPROVAL
        // ZADATAK 2.3.8
        // TODO: return changed offer
        [Route("project/offers/changeOffer/{id}/status/{status}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfferModel(int id, OfferModel.OfferStatus status)
        {
            if (db.offerModels.Find(id) == null) 
            {
                return NotFound();
            }

            OfferModel savedModel = db.offerModels.Find(id);
            savedModel.offer_status = status;
            db.Entry(savedModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferModelExists(id))
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

        // GET: project/offers/findByPrice/33.4/and/22.3/
        // ZADATAK 2.3.9
        [Route("project/offers/findByPrice/{lowerPrice}/and/{upperPrice}/")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<OfferModel>))]
        public IHttpActionResult GetOffersInGivenPriceRange(decimal lowerPrice, decimal upperPrice)
        {
            return Ok(db.offerModels.
                Where(o => o.action_price >= lowerPrice && o.action_price <= upperPrice).
                ToList());
        }

    }
}