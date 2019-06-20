using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Projekat.Models;
using Projekat.Repositories;

namespace Projekat.Controllers
{
    public class OfferController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: project/offers
        // ZADATAK 2.3.3
        [Route("project/offers")]
        [HttpGet]
        public IEnumerable<Offer> GetofferModels()
        {
            return db.OfferRepository.Get();
        }

        // GET: project/offers/3
        // ZADATAK 2.3.7
        [HttpGet]
        [Route("project/offers/{id}", Name = "SingleOfferById")]
        [ResponseType(typeof(Offer))]
        public IHttpActionResult GetOfferModel(int id)
        {
            Offer offerModel = db.OfferRepository.GetByID(id);
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
        public IHttpActionResult PutOfferModel(int id, Offer offerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offerModel.id)
            {
                return BadRequest();
            }


            db.OfferRepository.Update(offerModel);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: project/offers
        // ZADATAK 2.3.4
        [Route("project/offers")]
        [HttpPost]
        [ResponseType(typeof(Offer))]
        public IHttpActionResult PostOfferModel(Offer offerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OfferRepository.Insert(offerModel);
            db.Save();

            return CreatedAtRoute("SingleOfferById", new { id = offerModel.id }, offerModel);
        }

        // DELETE: project/offers/3
        // ZADATAK 2.3.6
        [Route("project/offers/{id}")]
        [HttpDelete]
        [ResponseType(typeof(Offer))]
        public IHttpActionResult DeleteOfferModel(int id)
        {
            Offer offerModel = db.OfferRepository.GetByID(id);
            if (offerModel == null)
            {
                return NotFound();
            }

            db.OfferRepository.Delete(offerModel);
            db.Save();

            return Ok(offerModel);
        }

        // PUT: project/offers/changeOffer/3/status/WAIT_FOR_APPROVAL
        // ZADATAK 2.3.8
        // TODO: return changed offer
        [Route("project/offers/changeOffer/{id}/status/{status}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfferModelChangeStatus(int id, Offer.OfferStatus status)
        {
            if (db.OfferRepository.GetByID(id) == null)
            {
                return NotFound();
            }

            Offer savedModel = db.OfferRepository.GetByID(id);
            savedModel.offer_status = status;
            // db.OfferRepository.Update(savedModel);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: project/offers/findByPrice/33.4/and/22.3/
        // ZADATAK 2.3.9
        [Route("project/offers/findByPrice/{lowerPrice}/and/{upperPrice}/")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Offer>))]
        public IHttpActionResult GetOffersInGivenPriceRange(decimal lowerPrice, decimal upperPrice)
        {
            return Ok(db.OfferRepository.Get(
                filter: o => o.action_price >= lowerPrice && o.action_price <= upperPrice));
        }

        // ONE LINE OF COMMENT ADDED.
        // FIRST TIME TO TRY BRANCHING, SAVING THE 'OLD' version

        // ZADATAK 3.2.2
        // PUT project/offers/{id}/updateCategory
        [Route("project/offers/{id}/updateCategory")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfferChangeCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Offer offerModel = db.OfferRepository.GetByID(id);
            if (offerModel == null)
            {
                return NotFound();
            }

            Category existingCategory = db.CategoryRepository.Get(
                filter: c => c.category_name == category.category_name &&
                    c.category_description == category.category_description).FirstOrDefault();

            if (existingCategory != null)
            {
                offerModel.categoryModel = existingCategory;
            }
            else
            {
                offerModel.categoryModel = category;
            }

            db.OfferRepository.Update(offerModel);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // mislim da iznad ipak treba preko categoryId ali videcemo...
        // na to me je navelo postojanje zadatka 3.2.4

        // ZADATAK 3.2.4
        // PUT project/offers/{id}/updateCategory
        [Route("project/offers/{offerId}/add-created-by/{userId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfferAddCreator(int offerId, int userId)
        {
            Offer offerModel = db.OfferRepository.GetByID(offerId);
            if (offerModel == null)
            {
                return NotFound();
            }

            // Treba li dodati logiku da se ne moze menjati created-by?
            User aUserModel = db.UserRepository.GetByID(userId);
            if (aUserModel == null)
            {
                return NotFound();
            }

            if (aUserModel.user_role != Models.User.UserRoles.ROLE_SELLER)
            {
                return base.BadRequest("User is not authorized to create an offer");
            }

            // offerModel.offer_created_by = userId;
            offerModel.userModel = aUserModel;
            // db.UserRepository.Update(userModel);
            Debug.WriteLine("OfferModel.UserModel is: " + offerModel.userModel.id);
            db.OfferRepository.Update(offerModel);

            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}