using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
        private IUserService userService;
        private ICategoryService categoryService;

        public OfferController(IUnitOfWork db, IOfferService offerService, IUserService userService, ICategoryService categoryService)
        {
            this.db = db;
            this.offerService = offerService;
            this.userService = userService;
            this.categoryService = categoryService;
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

            // Resenja se ovde razilaze
            // Kod mene nema provere da li pridruzena kategorija ili prodavac postoje
            // To ce dovesti do toga da ce sistem napraviti novu kategoriju
            // kao i novog prodavca
            // To ce se desiti i kada kategorija ili prodavac postoje
            // iz razloga jer ne postoji logika koja bi njih povezala sa postojecim entitetima

            // Kako u mom modelu nema CategoryId niti SellerId, logika bi bila ili u servisu ili
            // u kontroleru, ali znatno zamrsenija...

            // tipa: provera za kategoriju:
            // offerRepository.Get(filter: o => o.Name == ... && o.Description == ...)
            // ili pak, kroz Id, GetById((int)... kao i kod Mladena)
            // ali sa vaznom razlikom da tada moje resenje dodaje nove entitete
            // sto naravno, nije pozeljno!
            
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
            // Omoguciti dodavanje i izmenu kategorije i prodavca iz ponude
            // Bez Foreign Key svojstava stvari su mnogo komplikovane..
            // Sa Foreign Key JSON koji saljemo je znatno svedeniji ali je i logika bolja

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (!ModelState.IsValid || offerModel.categoryId == null || offerModel.userModel == null)

            // Mladen's code:

            // You also need to update your Model classes. Mladen's solution is to use 'normal' property
            // names and map them to the 'wacky' database column names
            if (!ModelState.IsValid || offerModel.categoryId == null || offerModel.sellerId == null)
            {
                return BadRequest(ModelState);
            }

            CategoryModel category = categoryService.GetCategory((int)offerModel.categoryId);
            UserModel seller = userService.GetUser((int)offerModel.sellerId);

            if (category == null || seller == null)
            {
                return NotFound();
            }

            if (seller.user_role != UserModel.UserRoles.ROLE_SELLER)
            {
                return BadRequest("User's role must be ROLE_SELLER");
            }

            offerModel.category = category;
            offerModel.seller = seller;
            OfferModel createdOffer = offerService.CreateOffer(offerModel);

            // my old stuff
            // offerService.CreateOffer(offerModel);

            return CreatedAtRoute("SingleOfferById", new { id = offerModel.id }, offerModel);

        }

        // DELETE: project/offers/3
        // ZADATAK 2.3.6
        [Route("project/offers/{id}")]
        [HttpDelete]
        [ResponseType(typeof(OfferModel))]
        public IHttpActionResult DeleteOfferModel(int id)
        {
            OfferModel offerModel = offerService.DeleteOffer(id);

            if (offerModel == null)
            {
                return NotFound();
            }

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
            OfferModel offer = offerService.UpdateOfferStatus(id, status);

            if (offer == null)
            {
                return NotFound();
            }

            return Ok(offer);
        }

        // GET: project/offers/findByPrice/33.4/and/22.3/
        // ZADATAK 2.3.9
        [Route("project/offers/findByPrice/{lowerPrice}/and/{upperPrice}/")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<OfferModel>))]
        public IHttpActionResult GetOffersInGivenPriceRange(decimal lowerPrice, decimal upperPrice)
        {
            return Ok(offerService.GetOffersByActionPriceRange(lowerPrice, upperPrice));
        }

        // These are NOT separate, distinct methods!
        // The logic should be built-in in the post (create) and put (update) methods
        // As for the bill-created flag and associated logic I need further analysis...
        #region Update category, associate seller
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
                offerModel.category = existingCategory;
            }
            else
            {
                offerModel.category = category;
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
            offerModel.seller = aUserModel;
            // db.UsersRepository.Update(userModel);
            Debug.WriteLine("OfferModel.UserModel is: " + offerModel.seller.id);
            db.OffersRepository.Update(offerModel);

            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion
    }
}