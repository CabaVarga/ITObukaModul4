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
using Project_3rd.Models;
using Project_3rd.Repositories;
using Project_3rd.Services;

namespace Project_3rd.Controllers
{
    public class OfferController : ApiController
    {
        private IUnitOfWork db;
        private IOfferService offerService;

        public OfferController(IUnitOfWork db, IOfferService offerService)
        {
            this.db = db;
            this.offerService = offerService;
        }

        // GET: project/offers
        // ZADATAK 2.3.3
        [Route("project/offers")]
        [HttpGet]
        public IEnumerable<OfferModel> GetofferModels()
        {
            return offerService.GetAllOffers();
        }

        // GET: project/offers/3
        // ZADATAK 2.3.7
        [HttpGet]
        [Route("project/offers/{id}", Name = "SingleOfferById")]
        [ResponseType(typeof(OfferModel))]
        public IHttpActionResult GetOfferModel(int id)
        {
            OfferModel offerModel = offerService.GetOffer(id);

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
            // Mladen's model is very different from mine
            // He's using explicit foreign keys
            // In the OfferModel these are the CategoryID and SellerID properties
            // THIS IS PROBABLY AN IMPORTANT QUESTION TO ASK!
            // These concepts are called INDEPENDENT vs FOREIGN KEY ASSOCIATIONS

            // TODO follow up from here

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // in the User controller this id check is inside the service
            if (id != offerModel.id)
            {
                return BadRequest();
            }


            offerService.UpdateOffer(id, offerModel);

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

            db.OffersRepository.Insert(offerModel);
            db.Save();

            return CreatedAtRoute("SingleOfferById", new { id = offerModel.id }, offerModel);
        }

        // DELETE: project/offers/3
        // ZADATAK 2.3.6
        [Route("project/offers/{id}")]
        [HttpDelete]
        [ResponseType(typeof(OfferModel))]
        public IHttpActionResult DeleteOfferModel(int id)
        {
            OfferModel offerModel = db.OffersRepository.GetByID(id);
            if (offerModel == null)
            {
                return NotFound();
            }

            db.OffersRepository.Delete(offerModel);
            db.Save();

            return Ok(offerModel);
        }

        // PUT: project/offers/changeOffer/3/status/WAIT_FOR_APPROVAL
        // ZADATAK 2.3.8
        // TODO: return changed offer
        [Route("project/offers/changeOffer/{id}/status/{status}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfferModelChangeStatus(int id, OfferModel.OfferStatus status)
        {
            if (db.OffersRepository.GetByID(id) == null)
            {
                return NotFound();
            }

            OfferModel savedModel = db.OffersRepository.GetByID(id);
            savedModel.offer_status = status;
            // db.OffersRepository.Update(savedModel);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: project/offers/findByPrice/33.4/and/22.3/
        // ZADATAK 2.3.9
        [Route("project/offers/findByPrice/{lowerPrice}/and/{upperPrice}/")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<OfferModel>))]
        public IHttpActionResult GetOffersInGivenPriceRange(decimal lowerPrice, decimal upperPrice)
        {
            return Ok(db.OffersRepository.Get(
                filter: o => o.action_price >= lowerPrice && o.action_price <= upperPrice));
        }

        // ONE LINE OF COMMENT ADDED.
        // FIRST TIME TO TRY BRANCHING, SAVING THE 'OLD' version

        // ZADATAK 3.2.2
        // PUT project/offers/{id}/updateCategory
        [Route("project/offers/{id}/updateCategory")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOfferChangeCategory(int id, CategoryModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OfferModel offerModel = db.OffersRepository.GetByID(id);
            if (offerModel == null)
            {
                return NotFound();
            }

            CategoryModel existingCategory = db.CategoriesRepository.Get(
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

            db.OffersRepository.Update(offerModel);
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
            OfferModel offerModel = db.OffersRepository.GetByID(offerId);
            if (offerModel == null)
            {
                return NotFound();
            }

            // Treba li dodati logiku da se ne moze menjati created-by?
            UserModel aUserModel = db.UsersRepository.GetByID(userId);
            if (aUserModel == null)
            {
                return NotFound();
            }

            if (aUserModel.user_role != UserModel.UserRoles.ROLE_SELLER)
            {
                return BadRequest("User is not authorized to create an offer");
            }

            // offerModel.offer_created_by = userId;
            offerModel.userModel = aUserModel;
            // db.UsersRepository.Update(userModel);
            Debug.WriteLine("OfferModel.UserModel is: " + offerModel.userModel.id);
            db.OffersRepository.Update(offerModel);

            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}